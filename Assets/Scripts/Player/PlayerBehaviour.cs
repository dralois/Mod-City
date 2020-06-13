using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private InputHandler inputHandler;
    private Rigidbody2D rigidbody;

    void Awake()
    {
        inputHandler = new InputHandler();
        rigidbody = GetComponent<Rigidbody2D>();
        inputHandler.Player.Jump.performed += _ => Jump();
    }

    void Jump()
    {
        if (OnGround())
        {
            rigidbody.velocity = new Vector2(0f, 5f);
        }
    }

    bool OnGround()
    {
        return rigidbody.velocity.y == 0f;
    }

    private void OnEnable()
    {
        inputHandler.Enable();
    }

    private void OnDisable()
    {
        inputHandler.Disable();
    }
}
