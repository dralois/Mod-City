using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Winable Game")]
public class Mod_Winable : IModObject
{
    protected override void AwakeInternal()
    {
        (Modable as WinObject).isWinable = true;
    }

    protected override void DestroyInternal()
    {
    }

    protected override void OnDisableInternal()
    {
    }

    protected override void OnEnableInternal()
    {
    }

    protected override void UpdateInternal()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
