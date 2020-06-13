using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Jump Mod")]
public class Mod_Jump : IModObject
{

	[SerializeField] private float jumpImpulse;
	[System.NonSerialized] private bool jumping;

	protected override void AwakeInternal()
	{
	}

	protected override void OnEnableInternal()
	{
		(Modable as PlayerBehaviour).InputHandler.Player.Jump.performed += Jump;
		(Modable as PlayerBehaviour).InputHandler.Player.Jump.Enable();
	}

	protected override void UpdateInternal()
	{
		if (jumping && (Modable as PlayerBehaviour).OnGround())
		{
			(Modable as PlayerBehaviour).PlayerRB.velocity =
				new Vector2((Modable as PlayerBehaviour).PlayerRB.velocity.x, jumpImpulse);
			(Modable as PlayerBehaviour).PlayerAnim.SetTrigger("Jump");
			(Modable as PlayerBehaviour).DirtParticles.Emit(10);
		}
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
		jumping = !jumping;
	}

}
