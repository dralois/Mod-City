using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerModTest : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    float f = 0;
    void Update()
    {
        if (f > 3)
        {
            ModManager.OnEvent(m => m.OnJump());
            float jumpHeight = 200F;
            jumpHeight = ModManager.ModifyValue((m, f) => m.ModifyJumpHeight(f), jumpHeight);
            rb.AddForce(Vector2.up * jumpHeight);
            f = 0;
        }

        f += Time.deltaTime;
    }
}
