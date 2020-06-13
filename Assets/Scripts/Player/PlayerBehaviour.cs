using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private InputHandler inputHandler;
    private Animator anim;
    private Rigidbody2D rb;
    private bool jumping;
    private float movement;
    private bool shooting;
    private bool isTurnedRight;
    private ParticleSystem dirt;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpImpuls;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Vector3 bulletOffset;
    
    private bool prevOnGround = false;
    
    void Awake()
    {
        inputHandler = new InputHandler();
        rb = GetComponent<Rigidbody2D>();

        isTurnedRight = true;
        anim = GetComponent<Animator>();
        dirt = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        ResetToSave();
    }

    void Update()
    {
        anim.SetFloat("Movement", Mathf.Max(0.05F, Mathf.Abs(movement * speed)));
        anim.SetBool("Air", !OnGround());

        if (prevOnGround != OnGround())
        {
            prevOnGround = !prevOnGround;
            if (!prevOnGround)
                dirt.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            else
                dirt.Play();
            dirt.Emit(10);
        }
        
        // Move
        transform.position = new Vector3(transform.position.x + movement * speed * Time.deltaTime, transform.position.y, 0);
        
        // Jump
        if (jumping && OnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpuls);
            anim.SetTrigger("Jump");
            dirt.Emit(10);
        }

        if (transform.position.y < -10)
            ResetToSave();
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

    void Jump(InputAction.CallbackContext cc)
    {
        jumping = !jumping;
    }

    bool OnGround()
    {
        return rb.velocity.y == 0f;
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

    public void ResetToSave()
    {
        rb.position = SavepointManager.Instance.lastSave.transform.position;
        rb.velocity = Vector2.zero;
        SavepointManager.Instance.OnReset();
    }

    private void OnEnable()
    {
        inputHandler.Player.Jump.performed += Jump;
        inputHandler.Player.Jump.Enable();

        inputHandler.Player.Movement.performed += Move;
        inputHandler.Player.Movement.Enable();

        inputHandler.Player.Shoot.performed += Shoot;
        inputHandler.Player.Shoot.Enable();
    }

    private void OnDisable()
    {
        inputHandler.Player.Jump.performed -= Jump;
        inputHandler.Player.Jump.Disable();

        inputHandler.Player.Movement.performed -= Move;
        inputHandler.Player.Movement.Disable();

        inputHandler.Player.Shoot.performed -= Shoot;
        inputHandler.Player.Shoot.Disable();
    }
}
