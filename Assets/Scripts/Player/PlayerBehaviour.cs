using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private InputHandler inputHandler;

    void Awake()
    {
        inputHandler = new InputHandler();
        inputHandler.Player.Jump.performed += _ => Jump();
    }

    void Jump()
    {
        Debug.Log("Jump");
    }

    bool OnGround()
    {
        return false;
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
