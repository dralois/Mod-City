using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private InputHandler inputHandler;
    private Rigidbody2D rb;
    private bool jumping;
    private float movement;
    private bool shooting;
    private bool isTurnedRight;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpImpuls;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Vector3 bulletOffset;
    
    void Awake()
    {
        inputHandler = new InputHandler();
        rb = GetComponent<Rigidbody2D>();

        isTurnedRight = true;
    }

    void Update()
    {
        // Move
        transform.position = new Vector3(transform.position.x + movement * speed * Time.deltaTime, transform.position.y, 0);
        
        // Jump
        if (jumping && OnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpuls);
        }
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
