using UnityEngine;
using System.Collections;

public class DecisionBool : Decision
{

    public bool valueDecision;
    public bool valueTest;

    public override Action GetBranch()
    {
        if (valueTest == valueDecision)
            return nodeTrue;
        return nodeFalse;
    }
}
