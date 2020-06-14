using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TurretController : IModable
{
    public float viewDistance = 7f;
    public float viewAngle = 30f;
    public float rotationSpeed = 2f;
    
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;
    public bool biggerAngle;

    public GameObject rotationPoint;
    public GameObject body;
    public GameObject trunk;
    public GameObject trunkStart;
    public GameObject trunkEnd;
    public Animator trunkAnimator;

    public PlayerTracker PlayerTracker;

    public GameObject projectilePrefab;
    public GameObject projectileSpawnPoint;
    public float projectileSpeed = 10f;
    
    private bool isRotatingToTheRight = true;

    private void Start()
    {
        PlayerTracker.setViewDistance(viewDistance);
        PlayerTracker.controller = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {      
        Vector3 gunLine = (trunkStart.transform.position-trunkEnd.transform.position).normalized*viewDistance;
        float rA = Vector3.SignedAngle(gunLine, patrolPoint1.transform.position - trunkEnd.transform.position, Vector3.forward);
        float lA = Vector3.SignedAngle(gunLine, patrolPoint2.transform.position - trunkEnd.transform.position, Vector3.forward);
        
        if (PlayerTracker.playerIsVisible)
        {
            float rD = Vector3.SignedAngle(gunLine, PlayerTracker.playerCollider.transform.position + Vector3.up * 0.5F - rotationPoint.transform.position, Vector3.forward);
            rD = Mathf.Sign(rD) * Mathf.Min(Mathf.Abs(rD), rotationSpeed / 5F * 4F);

            body.transform.RotateAround(rotationPoint.transform.position, new Vector3(0, 0, -1), -rD);
        }
        else
        {
            //Debug.Log(rA + " " + lA);

            if (!biggerAngle)
            {
                if (rA >= 0 || lA <= 0)
                {
                    switchDirection();
                }
            }
            else
            {
                if (!(rA >= 0 || lA <= 0))
                {
                    switchDirection();
                }
            }

            if (isRotatingToTheRight)
            {
                rotateRight();
            }
            else
            {
                rotateLeft();
            }
        }
    }
    
    private void rotateRight()
    {
        body.transform.RotateAround(rotationPoint.transform.position, new Vector3(0,0,-1), rotationSpeed/5);
    }
    
    private void rotateLeft()
    {
        body.transform.RotateAround(rotationPoint.transform.position, new Vector3(0,0,-1), -rotationSpeed/5);
    }

    private void switchDirection()
    {
        if (isRotatingToTheRight == true)
            isRotatingToTheRight = false;
        else
            isRotatingToTheRight = true;
    }


    private void OnDrawGizmos()    
    {
        Vector3 gunLine =  (trunkStart.transform.position-trunkEnd.transform.position).normalized*viewDistance;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(trunkStart.transform.position, (trunkStart.transform.position + gunLine).normalized*gunLine.magnitude);

        Gizmos.DrawLine(rotationPoint.transform.position, trunkStart.transform.position + Quaternion.AngleAxis(viewAngle, Vector3.forward) *gunLine);
        Gizmos.DrawLine(rotationPoint.transform.position, trunkStart.transform.position + Quaternion.AngleAxis(-viewAngle, Vector3.forward) *gunLine);
        
        Gizmos.DrawWireSphere(patrolPoint1.transform.position, 0.05f);
        Gizmos.DrawWireSphere(patrolPoint2.transform.position, 0.05f);
    }

    public void shootOnce()
    {
        trunkAnimator.Play("shot",  -1, 0f);
        
        Vector3 gunLine = trunkStart.transform.position-trunkEnd.transform.position;
        
        GameObject tmpProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.LookRotation(gunLine, Vector3.up));
        tmpProjectile.GetComponent<Projectile>().init(gunLine, projectileSpeed);
    }

    protected override void AwakeInternal()
    {
    }

    protected override void OnEnableInternal()
    {
    }

    protected override void UpdateInternal()
    {
    }

    protected override void OnDisableInternal()
    {
    }

    protected override void OnDestroyInternal()
    {
    }
}
