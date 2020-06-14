using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/SwitchGravity Mod")]
public class Mod_SwitchGravity : IModObject
{
	protected override void AwakeInternal()
	{
	}

	protected override void OnEnableInternal()
	{
		// (Modable as PlayerBehaviour).InputHandler.Player.Jump.performed += Jump;
		// (Modable as PlayerBehaviour).InputHandler.Player.Jump.Enable();
		(Modable as PlayerBehaviour).gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
	}

	protected override void UpdateInternal()
	{
		/*
if (jumping && (Modable as PlayerBehaviour).OnGround())
{
	(Modable as PlayerBehaviour).PlayerRB.velocity =
		new Vector2((Modable as PlayerBehaviour).PlayerRB.velocity.x, jumpImpulse);
	(Modable as PlayerBehaviour).PlayerAnim.SetTrigger("Jump");
	(Modable as PlayerBehaviour).DirtParticles.Emit(10);
}*/
	}

	protected override void OnDisableInternal()
	{
		// (Modable as PlayerBehaviour).InputHandler.Player.Jump.performed -= Jump;
		// (Modable as PlayerBehaviour).InputHandler.Player.Jump.Disable();
		(Modable as PlayerBehaviour).gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
	}

	protected override void DestroyInternal()
	{
	}

	void Jump(InputAction.CallbackContext cc)
	{
		// (Modable as PlayerBehaviour).jumped = true;
	}
}
