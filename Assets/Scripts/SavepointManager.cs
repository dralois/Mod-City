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
        DontDestroyOnLoad(this);
    }

    public void Save(Savepoint s)
    {
        lastSave.sprite.color = Color.green;
        s.sprite.color = Color.white;
        lastSave = s;
    }

    public void OnReset()
    {

    }
}
