using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTest : BasicAIBehavior {

    int shouldIUpdate = 300;

    public override bool NeedsUpdate()
    {
        if (shouldIUpdate > 0)
        {
            shouldIUpdate -= 1;
            return true;
        }
        return false;
    }

    public override void AIUpdate()
    {
        print("This is test manager number " + Priority + " reporting in!");
    }
}
