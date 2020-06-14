using Cinemachine;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class PlayerBehaviour : IModable
{
	private float movement, movementVal;
	private bool shooting;

	public bool onGround;
	private float lastOnGround = -100, lastInAir = -100;

	[Range(0, 20)]
	public float speed = 10;
	private float jumpForce;
	public float jumpHeight, jumpTime;
	public float movementSmoothAcc = 50;
	public float coyote;

	private Vector2 vel;
	private bool isTurnedRight = true;
	public bool jumped;

	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private Vector3 bulletOffset;

	private PostGlitch glitchSettings;

	public InputHandler InputHandler { get; private set; }
	public Animator PlayerAnim { get; private set; }
	public Rigidbody2D PlayerRB { get; private set; }
	public ParticleSystem DirtParticles { get; private set; }

	protected override void AwakeInternal()
	{
		InputHandler = new InputHandler();

		PlayerRB = GetComponent<Rigidbody2D>();
		PlayerAnim = GetComponent<Animator>();
		DirtParticles = GetComponentInChildren<ParticleSystem>();
		glitchSettings = Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<PostGlitch>();

		jumpForce = 4F * jumpHeight / jumpTime;
		Physics2D.gravity = Vector2.down * jumpForce * 2F / jumpTime;
	}

	private void Start()
	{
		ResetToSave();
	}

	protected override void UpdateInternal()
	{
	}

	void FixedUpdate()
	{
		//if (transform.position.y < -10)
		//ResetToSave();

		bool onGround = Physics2D.Raycast(new Vector2(PlayerRB.position.x + 0.25F * 0.15F, PlayerRB.position.y + 0.74F * 0.15F), Vector2.down, 0.05F);

		if (onGround != this.onGround)
		{
			this.onGround = onGround;
			if (!onGround)
				DirtParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
			else
				DirtParticles.Play();
			DirtParticles.Emit(10);
		}

		if (onGround)
		{
			vel.y = 0;
			lastOnGround = Time.time;
		}

		if (Mathf.Abs(movementVal) > 0.1F)
			movement = Mathf.Lerp(movement, movementVal, movementSmoothAcc * Time.fixedDeltaTime);
		else
			movement = movementVal;
		vel.x = movement * speed;

		if (movement >= 0.1F && !isTurnedRight)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			Turn();
		}
		if (movement <= -0.1F && isTurnedRight)
		{
			transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			Turn();
		}

		float jump = 0;
		if (Time.time - lastOnGround < coyote && jumped)
		{
			lastOnGround = -100;
			jump = jumpForce;

			PlayerAnim.SetTrigger("Jump");
			DirtParticles.Emit(10);
		}
		jumped = false;
		vel += new Vector2(0, jump);
		vel += Physics2D.gravity * Time.fixedDeltaTime;
		if (vel.y < 0)
			vel += Physics2D.gravity * 0.2F * Time.fixedDeltaTime;
		PlayerRB.velocity = vel;
		PlayerAnim.SetBool("Air", !onGround);
		PlayerAnim.SetFloat("Movement", Mathf.Max(0.05F, Mathf.Abs(movement * speed)));
	}

	public void Shoot()
	{
		// Single shoots
		Instantiate(bulletPrefab, transform.position + bulletOffset, transform.rotation);
	}

	void Turn()
	{
		bulletOffset = new Vector3(-bulletOffset.x, bulletOffset.y, 0);
		isTurnedRight = !isTurnedRight;
	}

	void Move(InputAction.CallbackContext cc)
	{
		movementVal = cc.ReadValue<float>();
	}

	public void ResetToSave()
	{
		StartCoroutine(DeathGlitch());
		PlayerRB.position = SavepointManager.Instance.lastSave.transform.position + Vector3.up * 0.75F;
		PlayerRB.velocity = vel = Vector2.zero;
		movement = 0;
		SavepointManager.Instance.OnReset();
	}

	IEnumerator DeathGlitch()
	{
		glitchSettings.enabled.value = true;
		for(int i = 0; i < 15; i++)
		{
			glitchSettings.glitch.value =  Mathf.PingPong(15f / i, 0.5f);
			yield return new WaitForSeconds(0.1f);
		}
		glitchSettings.enabled.value = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy") || collision.CompareTag("Deathzone"))
			ResetToSave();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.otherCollider.CompareTag("Enemy") || collision.otherCollider.CompareTag("Deathzone"))
			ResetToSave();
	}

	protected override void OnEnableInternal()
	{
		InputHandler.Player.Movement.performed += Move;
		InputHandler.Player.Movement.Enable();
	}

	protected override void OnDisableInternal()
	{
		InputHandler.Player.Movement.performed -= Move;
		InputHandler.Player.Movement.Disable();
	}

	protected override void OnDestroyInternal()
	{
	}
}
