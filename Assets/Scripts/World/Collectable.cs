using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Collectable : MonoBehaviour
{
    public float timeToCollect = 1;
    private Vector3 orgScale, orgPos;
    private Transform player;
    private Light2D light2D;

    void Start()
    {
        orgScale = transform.localScale;
        orgPos = transform.position;
        light2D = GetComponentInChildren<Light2D>();
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
            light2D.intensity = t;
            if (collectTick <= 0)
            {
                OnCollected();
                Destroy(gameObject);
            }
        }
        else
            transform.position = orgPos + Vector3.up * Mathf.Cos(Time.time * 5) * 0.1F;
    }

    private void OnCollected()
    {
        GetComponent<AudioSource>().Play();
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
