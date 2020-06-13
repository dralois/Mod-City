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

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpImpuls;
    [SerializeField]
    private GameObject bulletPrefab;

    void Awake()
    {
        inputHandler = new InputHandler();
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("Shoot");
        //Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    void Move(InputAction.CallbackContext cc)
    {
        movement = cc.ReadValue<float>();
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
