using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : IModable
{

	private float movement;
	private bool shooting;
	private bool isTurnedRight;
	private bool prevOnGround = false;

	[SerializeField]
	private float speed;
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private Vector3 bulletOffset;

	public InputHandler InputHandler{ get; private set; }
	public Animator PlayerAnim { get; private set; }
	public Rigidbody2D PlayerRB { get; private set; }
	public ParticleSystem DirtParticles { get; private set; }

	protected override void Awake()
	{
		InputHandler = new InputHandler();

		isTurnedRight = true;
		PlayerRB = GetComponent<Rigidbody2D>();
		PlayerAnim = GetComponent<Animator>();
		DirtParticles = GetComponentInChildren<ParticleSystem>();

		base.Awake();
	}

	private void Start()
	{
		ResetToSave();
	}

	protected override void Update()
	{
		PlayerAnim.SetFloat("Movement", Mathf.Max(0.05F, Mathf.Abs(movement * speed)));
		PlayerAnim.SetBool("Air", !OnGround());

		if (prevOnGround != OnGround())
		{
			prevOnGround = !prevOnGround;
			if (!prevOnGround)
				DirtParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
			else
				DirtParticles.Play();
			DirtParticles.Emit(10);
		}

		// Move
		transform.position = new Vector3(transform.position.x + movement * speed * Time.deltaTime, transform.position.y, 0);

		if (transform.position.y < -10)
			ResetToSave();

		base.Update();
	}

	void Shoot(InputAction.CallbackContext cc)
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
		movement = cc.ReadValue<float>();
		if (movement == 1 && !isTurnedRight)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			Turn();
		}
		if (movement == -1 && isTurnedRight)
		{
			transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			Turn();
		}
	}

	public bool OnGround()
	{
		return PlayerRB.velocity.y == 0f;
	}

	public void ResetToSave()
	{
		PlayerRB.position = SavepointManager.Instance.lastSave.transform.position;
		PlayerRB.velocity = Vector2.zero;
		SavepointManager.Instance.OnReset();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
			ResetToSave();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.otherCollider.CompareTag("Enemy"))
			ResetToSave();
	}

	protected override void OnEnable()
	{
		InputHandler.Player.Movement.performed += Move;
		InputHandler.Player.Movement.Enable();

		InputHandler.Player.Shoot.performed += Shoot;
		InputHandler.Player.Shoot.Enable();

		base.OnEnable();
	}

	protected override void OnDisable()
	{
		InputHandler.Player.Movement.performed -= Move;
		InputHandler.Player.Movement.Disable();

		InputHandler.Player.Shoot.performed -= Shoot;
		InputHandler.Player.Shoot.Disable();

		base.OnDisable();
	}
}
