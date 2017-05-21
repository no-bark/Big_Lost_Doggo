using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAvoidance : BasicAIBehavior
{
    bool collidedThisFrame;

    Vector3 AwayVec;

    void LateUpdate()
    {
        //clear the colliders, set collided this frame to false
    }

    public override bool NeedsUpdate()
    {
        if(collidedThisFrame)
        {
            return true;
        }
        return false;
    }

    public override void AIUpdate()
    {
        //move toward the awayvec
        print("Parent class needs to modify \"AIUpdate\" function.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //add direction to the awayvec
    }
}
