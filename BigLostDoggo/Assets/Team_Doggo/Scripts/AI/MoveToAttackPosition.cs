using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAttackPosition : BasicAIBehavior
{
    Targeted myTargeted;

    public float validDistance = 7.0f;
    float timer = 0.0f;
    
    public float speed = 2.0f;

    bool cooldown = false;
    bool attacking = false;

    void Start()
    {
        myTargeted = GetComponent<Targeted>();
    }

    void Update()
    {
    }

    public override bool NeedsUpdate()
    {
        if (myTargeted.tweenDist < validDistance && !cooldown)
        {
            if(attacking)
            {
                return true;
            }

            print("Getting ready to attack!");
            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
                return true;
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
            transform.position = transform.position + (myTargeted.tweenVec) * (speed * (Mathf.Min(myTargeted.tweenDist, 2)) * Time.deltaTime);
            if (myTargeted.tweenDist < 1)
            {
                print("Attack!!");
                StartCoroutine("Cooldown");
            }
        }
    }

    IEnumerator Cooldown()
    {
        attacking = true;
        yield return new WaitForSeconds(1);
        cooldown = true;
        attacking = false;
        yield return new WaitForSeconds(6);
        cooldown = false;
    }
}
