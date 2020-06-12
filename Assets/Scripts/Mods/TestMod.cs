using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMod : ModBase
{
    public override float ModifyJumpHeight(float org)
    {
        if (version == 0)
            return org * 2;
        else
            return org * 3;
    }
}
