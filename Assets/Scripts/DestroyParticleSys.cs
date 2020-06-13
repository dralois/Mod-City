using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticleSys : MonoBehaviour
{
    private ParticleSystem sys;

    void Start()
    {
        sys = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!sys.isPlaying)
            Destroy(gameObject);
    }
}
