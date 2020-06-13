using UnityEngine;

[System.Serializable]
public abstract class IModObject : ScriptableObject
{

	[SerializeField] private Sprite _icon;
	[SerializeField] private string _name;
	[SerializeField] private string _author;
	[SerializeField] private int _version;
	[SerializeField] private bool _requiresRestart;
	[SerializeField] private IModObject[] _dependencies = { };
	[SerializeField] private IModObject[] _incompatibles = { };

	public Sprite Icon { get => _icon; }
	public string Name { get => _name; }
	public string Author { get => _author; }
	public int Version { get => _version; }
	public IModObject[] Dependencies { get => _dependencies; }
	public IModObject[] Incompatibles { get => _incompatibles; }

	public bool IsLoaded { get; set; } = false;
	public bool IsActivated { get; set; } = false;
	public IModable Modable { get; private set; }

	protected abstract void EnableInternal();

	protected abstract void UpdateInternal();

	protected abstract void DisableInternal();

	public bool ModLoad()
	{
		return ModHandler.Instance.TryLoadMod(this);
	}

	public bool ModUnload()
	{
		return ModHandler.Instance.TryUnloadMod(this);
	}

	public bool ModEnable()
	{
		if (IsLoaded && !IsActivated && ModHandler.Instance.TryEnableMod(this))
		{
			EnableInternal();
			return true;
		}
		else
		{
			Debug.Log($"Couldnt enable mod {this} for {Modable}", this);
			return false;
		}
	}

	public void ModSetup(IModable myModable)
	{
		Modable = myModable;
		Debug.Log($"Setup mod {this} for {Modable as ModTester}", this);
	}

	public void ModUpdate()
	{
		if (IsLoaded && IsActivated)
		{
			UpdateInternal();
		}
	}

	public bool ModDisable()
	{
		if (IsLoaded && IsActivated && ModHandler.Instance.TryDisableMod(this))
		{
			DisableInternal();
			Modable = null;
			return true;
		}
		else
		{
			Debug.Log($"Couldnt disable mod {this} for {Modable}", this);
			return false;
		}
	}
}
