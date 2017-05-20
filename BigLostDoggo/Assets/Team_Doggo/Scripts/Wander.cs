using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : BasicAIBehavior
{
    Vector3 CurFloatDirection;

    public float speed;
    public float randSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        AIUpdate();
    }

    public override bool NeedsUpdate()
    {
        return true;
    }

    public override void AIUpdate()
    {
        CurFloatDirection = (CurFloatDirection + new Vector3(Random.Range(-randSpeed, randSpeed), Random.Range(-randSpeed, randSpeed), Random.Range(-randSpeed, randSpeed)));

        CurFloatDirection = CurFloatDirection.normalized;

        transform.position = transform.position + (CurFloatDirection * (speed));
    }
}
