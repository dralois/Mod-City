using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Real Pause Menu")]
public class Mod_MenuPause : IModObject
{
    protected override void AwakeInternal()
    {
        (Modable as PauseScreen).timepause = 0.0003f;
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
}
