using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAvoidance : BasicAIBehavior
{
    bool collidedThisFrame;
    Vector3 AwayVec;

    public float speed = 2;

    Rigidbody2D myBody;

    void LateUpdate()
    {
        collidedThisFrame = false;
        AwayVec = new Vector3(0, 0, 0);
        //clear the colliders, set collided this frame to false
    }

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    public override bool NeedsUpdate()
    {
        if(collidedThisFrame)
        {
            AIUpdate();
            return true;
        }
        return false;
    }

    public override void AIUpdate()
    {
        myBody.MovePosition(transform.position + (AwayVec * Time.deltaTime * speed));
        //move toward the awayvec
        print("Parent class needs to modify \"AIUpdate\" function.");
    }

    void OnCollisionStay2D(Collision2D other)
    {
        print("Collided!");
        //add direction to the awayvec
        Vector3 newAwayVec = other.contacts[0].normal;
        newAwayVec = newAwayVec.normalized;
        newAwayVec *= 0.3f - (other.contacts[0].point - new Vector2(transform.position.x, transform.position.y)).magnitude;
        AwayVec += newAwayVec;
        collidedThisFrame = true;
    }
}
