using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAttackPosition : BasicAIBehavior
{
    Targeted myTargeted;
    BehaviorManager myBehaviorManager;
    public float validDistance = 7.0f;
    float timer = 0.0f;
    
    public float speed = 2.0f;

    bool cooldown = false;
    bool attacking = false;
    Rigidbody2D myBody;

    void Start()
    {
        myTargeted = GetComponent<Targeted>();
        myBehaviorManager = GetComponent<BehaviorManager>();
        myTargeted = this.GetComponent<Targeted>();
    }

    void Update()
    {
    }

    public override bool NeedsUpdate()
    {
        if (myTargeted.tweenDist < validDistance && !cooldown)
        {
            print("Getting ready to attack!");
            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
                myBehaviorManager.actingBehavior = this;
            }
        }
        else
        {
            timer = 0;
        }
        return false;
    }

    public override void AIUpdate()
    {
        if (!attacking)
        {
            myBody.MovePosition(transform.position + (myTargeted.tweenVec) * (speed * (Mathf.Min(myTargeted.tweenDist, 2)) * Time.deltaTime));
            if (myTargeted.tweenDist < 1)
            {
                print("Attack!!");
                StartCoroutine("Cooldown");
            }
        }
    }

    IEnumerator Cooldown()
    {
        timer = 0;
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
        myBehaviorManager.actingBehavior = null;
        cooldown = true;
        yield return new WaitForSeconds(6);
        cooldown = false;
    }
}
