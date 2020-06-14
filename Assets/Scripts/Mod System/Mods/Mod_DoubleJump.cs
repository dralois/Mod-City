using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Double Jump Mod")]
public class Mod_DoubleJump : IModObject
{
    private bool jumped = false;

	protected override void AwakeInternal()
	{
	}

	protected override void OnEnableInternal()
	{
		(Modable as PlayerBehaviour).InputHandler.Player.Jump.performed += Jump;
		(Modable as PlayerBehaviour).InputHandler.Player.Jump.Enable();
        jumped = false;
	}

	protected override void UpdateInternal()
	{
        if ((Modable as PlayerBehaviour).onGround)
            jumped = false;
	}

	protected override void OnDisableInternal()
	{
		(Modable as PlayerBehaviour).InputHandler.Player.Jump.performed -= Jump;
		(Modable as PlayerBehaviour).InputHandler.Player.Jump.Disable();
	}

	protected override void DestroyInternal()
	{
	}

	void Jump(InputAction.CallbackContext cc)
	{
        if (!jumped && !(Modable as PlayerBehaviour).onGround)
        {
            jumped = true;
            (Modable as PlayerBehaviour).lastOnGround = Time.time;
            (Modable as PlayerBehaviour).jumped = true;
        }
    }
}
