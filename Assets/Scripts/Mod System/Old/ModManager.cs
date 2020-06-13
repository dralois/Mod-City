using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static Mod[] modRegistry = new Mod[]
    {
        new Mod("test", "Test Mod", "Hendrik", new TestMod()).SetVersions("0.1", "1.0-alpha"),
        new Mod("test2", "Test Mod 2", "Hendrik", new TestMod()).AddDependency("test"),
        new Mod("test3", "Test Mod 3", "Hendrik", new TestMod()).MakeIncompatible("test", 1).RequireRestart(),
    };

    public static Dictionary<string, Mod> modDict = new Dictionary<string, Mod>();

    private static ModBase[] activeMods = new ModBase[0];
    private static ModManager instance;
    public static ModManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("Singleton ModManager does not exist :/");
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this); // Just in case

            foreach (Mod m in modRegistry)
                modDict.Add(m.id, m);

            List<ModVersion> mods = new List<ModVersion>
            {
                new ModVersion("test", 1),
            };
            ActivateMods(mods);
        }
    }

    // A bit slow if we have a lot of mods
    public static bool CheckCompatible(List<ModVersion> mods)
    {
        foreach (ModVersion m in mods)
        {
            Mod mod = modDict[m.id];
            foreach (ModVersion d in mod.dependencies)
            {
                bool satisfied = false;
                foreach (ModVersion m2 in mods)
                    if (m2.id.Equals(d.id))
                    {
                        if (m2.version >= d.version)
                        {
                            satisfied = true;
                            break;
                        }
                        else
                            return false;
                    }

                if (!satisfied)
                    return false;
            }
            foreach (ModVersion d in mod.incompatible)
            {
                foreach (ModVersion m2 in mods)
                    if (m2.id.Equals(d.id))
                    {
                        if (m2.version >= d.version)
                            return false;
                        break;
                    }
            }
        }
        return true;
    }

    // Returns false if some mods are incompatible => crash :D
    public static bool ActivateMods(List<ModVersion> mods)
    {
        if (!CheckCompatible(mods))
        {
            Debug.Log("Mods are incompatible.");
            return false;
        }

        activeMods = new ModBase[mods.Count];
        for (int i = 0; i < mods.Count; ++i)
        {
            activeMods[i] = modDict[mods[i].id].modObject;
            activeMods[i].version = mods[i].version;
            activeMods[i].OnModActivate();
        }

        return true;
    }

    public static void OnEvent(Action<ModBase> action)
    {
        foreach (ModBase mod in activeMods)
            action.Invoke(mod);
    }

    public static float ModifyValue(Func<ModBase, float, float> modifier, float b)
    {
        foreach (ModBase mod in activeMods)
            b = modifier.Invoke(mod, b);

        return b;
    }

    public static int ChooseValue(Func<ModBase, int> modifier, int def)
    {
        int val = def;

        foreach (ModBase mod in activeMods)
        {
            int i = modifier.Invoke(mod);
            if (i > 0)
                val = i;
        }

        return val;
    }

    [Serializable]
    public class ModVersion
    {
        public string id;
        public int version;

        public ModVersion(string id, int version = 0)
        {
            this.id = id;
            this.version = version;
        }
    }

    public class Mod
    {
        public string id, name, author;
        public string[] versions = new string[] { "1.0" };
        public List<ModVersion> dependencies = new List<ModVersion>();
        public List<ModVersion> incompatible = new List<ModVersion>();
        public ModBase modObject;
        public bool requiresRestart = false;

        public Mod(string id, string name, string author, ModBase modObject)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.modObject = modObject;
        }

        public Mod SetVersions(params string[] versions)
        {
            this.versions = versions;
            return this;
        }

        // Use index of version instead of string
        public Mod AddDependency(string id, int minVersion = 0)
        {
            dependencies.Add(new ModVersion(id, minVersion));
            return this;
        }

        public Mod MakeIncompatible(string id, int minVersion = 0)
        {
            incompatible.Add(new ModVersion(id, minVersion));
            return this;
        }

        public Mod RequireRestart()
        {
            requiresRestart = true;
            return this;
        }
    }
}
