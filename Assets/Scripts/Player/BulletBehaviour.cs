using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lifetime;

    public GameObject hitPrefab;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            Destroy(gameObject);
        else
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
