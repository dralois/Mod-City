using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModBase
{
    public int version;

    public virtual void OnLevelStart()
    {

    }

    public virtual void OnModActivate()
    {
        Debug.Log("Activated: " + this);
    }

    // Add Moddable functions here, override in new child class of ModBase
    public virtual void OnJump()
    {

    }

    public virtual float ModifyJumpHeight(float org)
    {
        return org;
    }

    public virtual float ModifyMovementSpeed(float org)
    {
        return org;
    }

    public virtual float ModifyCameraShake(float org)
    {
        return org;
    }

    public virtual int ChooseSkin()
    {
        return -1;
    }
}
