using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIBehavior : MonoBehaviour {

    public int Priority;

    public virtual bool NeedsUpdate()
    {
        print("Parent class needs to modify \"Needs update\" function.");
        return true;
    }

    public virtual void AIUpdate()
    {
        print("Parent class needs to modify \"AIUpdate\" function.");
    }

    void Awake()
    {
        if (GetComponent<BehaviorManager>() == null)
        {
            print("Error: behavior added but no behavior manager!");
        }
        else
        {
            GetComponent<BehaviorManager>().addBehavior(this);
        }
    }
}

public class BehaviorCompare : Comparer<BasicAIBehavior>
{
    // Compares by Length, Height, and Width.
    public override int Compare(BasicAIBehavior x, BasicAIBehavior y)
    {
        return y.Priority.CompareTo(x.Priority);
    }

}