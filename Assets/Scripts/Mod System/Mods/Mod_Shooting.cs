using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Shooter Mod")]
public class Mod_Shooting : IModObject
{
    protected override void AwakeInternal()
    {
    }

    protected override void OnEnableInternal()
    {
        (Modable as PlayerBehaviour).InputHandler.Player.Shoot.performed += Shoot;
        (Modable as PlayerBehaviour).InputHandler.Player.Shoot.Enable();
    }

    protected override void UpdateInternal()
    {
    }

    protected override void OnDisableInternal()
    {
        (Modable as PlayerBehaviour).InputHandler.Player.Shoot.performed -= Shoot;
        (Modable as PlayerBehaviour).InputHandler.Player.Shoot.Disable();
    }

    protected override void DestroyInternal()
    {
    }

    void Shoot(InputAction.CallbackContext cc)
    {
        (Modable as PlayerBehaviour).Shoot();
    }
}
