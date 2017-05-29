using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepsDistanceFromPlayer : BasicAIBehavior
{
    Targeted myTargeted;
    Vector3 desiredPosition;

    public float speed = 2.0f;
    public float desiredDist = 4.0f;
    public float LocationUpdateFrequency = 0.2f;
    
    Vector3 CurFloatDirection;

    public float wanderSpeed;
    public float randSpeed;
    Rigidbody2D myBody;

    Coroutine updateCoroutine;
	// Use this for initialization
	void Start ()
    {
        myBody = GetComponent<Rigidbody2D>();
        myTargeted = this.GetComponent<Targeted>();
        updateCoroutine = StartCoroutine("UpdateDesiredLocation");
	}
	
    IEnumerator UpdateDesiredLocation()
    {
        while(true)
        {
            desiredPosition = myTargeted.target.transform.position - (myTargeted.tweenVec * this.desiredDist);
            yield return new WaitForSeconds(LocationUpdateFrequency);
        }
    }

    void Update()
    {
    }

    public override bool NeedsUpdate()
    {
        return true;
    }

    public override void AIUpdate()
    {
        myBody.MovePosition(transform.position + (desiredPosition - transform.position).normalized * (speed * (Mathf.Min((desiredPosition - transform.position).magnitude, 15) / 15)) * Time.deltaTime);
        
        CurFloatDirection = (CurFloatDirection + new Vector3(Random.Range(-randSpeed, randSpeed), Random.Range(-randSpeed, randSpeed), 0));
        CurFloatDirection = CurFloatDirection.normalized;
        myBody.MovePosition(transform.position + (CurFloatDirection * (wanderSpeed)) * Time.deltaTime * (1 - (Mathf.Min((desiredPosition - transform.position).magnitude, 15) / 15)));
    }
}
