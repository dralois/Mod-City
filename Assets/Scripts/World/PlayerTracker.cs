using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public bool drawRaysGizmos = true;
    public int numberOfRays = 5;
    
    
    private const float fireRayPeriod = 0.3f;
    private Collider2D playerCollider;

    private IEnumerator fireRaysCoroutine;

    private float viewDistance = 0;
    private bool playerIsVisible = false;

    public void setViewDistance(float vd)
    {
        viewDistance = vd;
        GetComponent<CircleCollider2D>().radius = vd;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerCollider = other;
            fireRaysCoroutine = fireRays(fireRayPeriod);
            StartCoroutine(fireRaysCoroutine);
        }
        Debug.Log("tr enter");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = null;
            StopCoroutine(fireRaysCoroutine);
            //other.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        Debug.Log("tr exit");
    }


    private void FixedUpdate()
    {
        if (playerIsVisible)
        {
            //TODO turn gun to player

            Debug.DrawLine(this.transform.position, playerCollider.gameObject.transform.position, Color.red);
        }
    }

    private void CheckVision()
    {
        
        if (playerCollider != null)
        { 
            //start turning to player
            //TODO
            Vector3 playerPos = playerCollider.gameObject.transform.position;
            
            RaycastHit2D hit;
            if (FireRaycast(playerCollider.gameObject, numberOfRays, out hit))
            {
                Debug.Log("player in direct view");
                
            }
        }     
        
    }
    
    
    private bool FireRaycast(GameObject target, int raysCount, out RaycastHit2D outHit) {
        
        for (int i = 0; i < raysCount; i++) {
         
            Bounds bounds = target.GetComponent<SpriteRenderer>().bounds;
            float y = bounds.extents.y;
            float x = bounds.extents.x;

            //Debug.Log(bounds.size);


            Vector3 randomDisplacement = new Vector3(UnityEngine.Random.Range(-x, x), UnityEngine.Random.Range(-y, y), 0);
            //Debug.DrawLine(this.transform.position, target.transform.position + randomDisplacement, Color.green, Time.deltaTime);
            //Debug.Log("Random disp: "+randomDisplacement);


            outHit = Physics2D.Raycast(this.transform.position,target.transform.position - this.transform.position + randomDisplacement, viewDistance);
            if (outHit.collider != null)
            {
                if (outHit.collider.gameObject.Equals(target.gameObject) /*&& !entitiesWeSee.Contains(outHit.collider.gameObject)*/)
                {
                    playerIsVisible = true;
                    return true;
                }
            }
        }

        playerIsVisible = false;
        outHit = new RaycastHit2D();
        return false;
    }
    
    
    IEnumerator fireRays(float time) {
        while (true) {
            CheckVision();
            yield return new WaitForSeconds(time);
        }
    }
}
