using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAttackPosition : BasicAIBehavior
{
    Targeted myTargeted;

    public float validDistance = 7.0f;
    public float timer = 0.0f;

    public bool movingToAttack = false;
    public float speed = 2.0f;

    void Start()
    {
        myTargeted = GetComponent<Targeted>();
    }

    void Update()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);
    }

    public override bool NeedsUpdate()
    {
        if (myTargeted.tweenDist < validDistance && !movingToAttack)
        {
            timer += 2 * Time.deltaTime;
        }
        else
        {
            timer = 3;
        }

        if(timer > 4.0f)
        {
            timer = 0;
            movingToAttack = true;
            return true;
        }
        if(movingToAttack)
        {
            return true;
        }

        return false;
    }

    public override void AIUpdate()
    {
        transform.position = transform.position + (myTargeted.tweenVec) * (speed * (Mathf.Min(myTargeted.tweenDist, 2)));
    }
}
