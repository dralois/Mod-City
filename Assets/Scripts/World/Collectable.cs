using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float timeToCollect = 1;
    private Vector3 orgScale, orgPos;
    private Transform player;

    void Start()
    {
        orgScale = transform.localScale;
        orgPos = transform.position;
    }

    float collectTick = 0;
    void Update()
    {
        if (collectTick > 0)
        {
            collectTick -= Time.deltaTime / timeToCollect;
            float t = collectTick;
            transform.position = Vector3.Lerp(player.position + Vector3.up * 0.5F, orgPos, t * t);
            transform.localScale = orgScale * t;
            if (collectTick <= 0)
            {
                OnCollected();
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * 180F);
            transform.position = orgPos + Vector3.up * Mathf.Cos(Time.time * 5) * 0.1F;
        }
    }

    private void OnCollected()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collectTick <= 0 && collision.CompareTag("Player"))
        {
            orgPos = transform.position;
            player = collision.transform;
            collectTick = 1;
        }
    }
}
