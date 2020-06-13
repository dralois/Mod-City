using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicRobot : MonoBehaviour
{
    public Vector3 pos1, pos2;

    public float movementSpeed;
    private float pathTime;
    private Rigidbody2D rb;

    private void Start()
    {
        if (pos1.x > pos2.x)
        {
            Vector3 pos3 = pos1;
            pos1 = pos2;
            pos2 = pos3;
        }

        pathTime = (pos2 - pos1).magnitude / movementSpeed;
        cycleTick = (transform.position.x - pos1.x) / (pos2.x - pos1.x);
        rb = GetComponent<Rigidbody2D>();
    }

    private float cycleTick;

    void Update()
    {
        if (cycleTick > 0)
        {
            cycleTick -= Time.deltaTime / pathTime;
            rb.position = new Vector3(Mathf.Lerp(pos1.x, pos2.x, Interpolate(cycleTick)), transform.position.y, transform.position.z);

            if (cycleTick <= 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            cycleTick -= Time.deltaTime / pathTime;
            rb.position = new Vector3(Mathf.Lerp(pos1.x, pos2.x, Interpolate(-cycleTick)), transform.position.y, transform.position.z);

            if (cycleTick <= -1)
            {
                cycleTick += 2;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    // Sigmoid interpolation for fancy ease-in-ease-out animation
    private static readonly float sigmoidK = 3;
    private static readonly float correction = 0.5F / Sigmoid(1F);
    private static float Interpolate(float f)
    {
        return correction * Sigmoid(2 * f - 1) + 0.5F;
    }

    private static float Sigmoid(float t)
    {
        return (1F / (1F + Mathf.Exp(-sigmoidK * t))) - 0.5F;
    }
}
