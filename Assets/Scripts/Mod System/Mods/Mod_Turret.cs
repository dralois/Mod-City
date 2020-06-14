using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Turret Mod")]
public class Mod_Turret : IModObject
{
    protected override void AwakeInternal()
    {
    }

    protected override void OnEnableInternal()
    {
        (Modable as TurretController).PlayerTracker.fireRayPeriod *= 3;

    }

    protected override void UpdateInternal()
    {
    }

    protected override void OnDisableInternal()
    {

    }

    protected override void DestroyInternal()
    {
    }

}
