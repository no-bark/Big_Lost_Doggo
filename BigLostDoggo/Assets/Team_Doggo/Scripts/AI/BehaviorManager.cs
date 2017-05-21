using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BehaviorManager : MonoBehaviour {

    List<BasicAIBehavior> behaviorsList = new List<BasicAIBehavior>();
	// Use this for initialization
	void Start ()
    {
        behaviorsList.Sort(new BehaviorCompare());
	}

    public void addBehavior(BasicAIBehavior b)
    {
        behaviorsList.Add(b);
    }

	// Update is called once per frame
	void Update ()
    {
        //loop through our behaviors this frame
		for(int i = 0; i < behaviorsList.Count; ++i)
        {
            //if our behavior needs to be done, that's the priority for this frame!
            BasicAIBehavior behavior = behaviorsList[i];
            if (behavior.NeedsUpdate())
            {
                //update the thing
                behaviorsList[i].AIUpdate();

                int goPriority = behavior.Priority;
                int j = i - 1;

                //do the behaviors before the selected behavior that are the same group
                while(j >= 0 && behaviorsList[j].Priority == goPriority)
                {
                    behaviorsList[j].NeedsUpdate();
                    behaviorsList[j].AIUpdate();
                    --j;
                }
                //do the same for the behaviors after.
                j = i + 1;
                while(j < behaviorsList.Count && behaviorsList[j].Priority == goPriority)
                {
                    behaviorsList[j].NeedsUpdate();
                    behaviorsList[j].AIUpdate();
                    ++j;
                }
                 
                //don't check any more behaviors
                break;
            }
        }
	}
}
