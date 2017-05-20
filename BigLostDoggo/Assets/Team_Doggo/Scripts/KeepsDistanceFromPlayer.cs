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

    Coroutine updateCoroutine;
	// Use this for initialization
	void Start () {
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
        AIUpdate();
    }

    public override bool NeedsUpdate()
    {
        return true;
    }

    public override void AIUpdate()
    {
        transform.position = transform.position + (desiredPosition - transform.position).normalized * (speed * (Mathf.Min((desiredPosition - transform.position).magnitude, 2) / 2));
    }
}
