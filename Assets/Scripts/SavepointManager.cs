using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavepointManager : Singleton<SavepointManager>
{
    public Savepoint inital;
    public Savepoint lastSave;

    private void Start()
    {
        lastSave = inital;
        lastSave.anim.SetBool("active", true);
        lastSave.sys.Play();

        DontDestroyOnLoad(this);
    }

    public void Save(Savepoint s)
    {
        lastSave.anim.SetBool("active", false);
        lastSave.sys.Stop();
        s.anim.SetBool("active", true);
        s.sys.Play();
        lastSave = s;
    }

    public void OnReset()
    {

    }
}
