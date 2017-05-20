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
}
