using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavepointManager : Singleton<SavepointManager>
{
    public Savepoint inital;
    public Savepoint lastSave;
    public Vector3 savePos;

    private void Awake()
    {
        lastSave = inital;
        savePos = lastSave.transform.position;
        lastSave.anim.SetBool("active", true);
        lastSave.sys.Play();

        DontDestroyOnLoad(this);
    }

    public void Save(Savepoint s)
    {
        if (lastSave != null)
        {
            lastSave.anim.SetBool("active", false);
            lastSave.sys.Stop();
        }
        s.anim.SetBool("active", true);
        s.sys.Play();
        lastSave = s;
        savePos = lastSave.transform.position;
    }

    public void OnReset()
    {

    }
}
