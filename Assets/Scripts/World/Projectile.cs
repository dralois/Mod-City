using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifetime;

    public GameObject hitPrefab;

    public void init(Vector3 direction, float speed)
    {
        direction = direction.normalized;

        float w = Vector3.SignedAngle(new Vector3(-1, 0, 0), direction, Vector3.forward);
        transform.eulerAngles = new Vector3(0,0, w);
        
        this.GetComponent<Rigidbody2D>().AddForce(10*speed*direction);
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Untagged"))
        {
            Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
