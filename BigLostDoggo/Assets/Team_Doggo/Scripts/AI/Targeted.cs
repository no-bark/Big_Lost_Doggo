using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeted : MonoBehaviour {
    public GameObject target = null;

    public Vector3 tweenVec = new Vector3();
    public float tweenDist = 0;

    public delegate void TargetChangedEvent();
    public event TargetChangedEvent onTargetChanged;

	// Use this for initialization
	void Start ()
    {
		
	}
	
    void CalculateStats()
    {
        tweenVec = (target.transform.position - transform.position);
        tweenDist = tweenVec.magnitude;
        tweenVec = tweenVec.normalized;
    }

    public void TargetChange(GameObject newTarget)
    {
        CalculateStats();
        onTargetChanged();
    }
	// Update is called once per frame
	void Update ()
    {
        CalculateStats();
	}
}
