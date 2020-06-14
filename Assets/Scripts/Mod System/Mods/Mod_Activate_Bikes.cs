using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Active Plattforms")]

public class Mod_Activate_Bikes : IModObject
{
    protected override void AwakeInternal()
    {
        ActivateBike bike = (Modable as ActivateBike);
        bike.GetComponent<Collider2D>().enabled = true;
        bike.GetComponent<SpriteRenderer>().enabled = true;
    }

    protected override void DestroyInternal()
    {
    }

    protected override void OnDisableInternal()
    {
    }

    protected override void OnEnableInternal()
    {
    }

    protected override void UpdateInternal()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
