using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lifetime;

    [SerializeField]
    private GameObject hitPrefab;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
