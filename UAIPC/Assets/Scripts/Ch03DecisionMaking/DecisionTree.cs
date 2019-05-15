using UnityEngine;
using System.Collections;

public class DecisionTree : DecisionTreeNode
{
    public DecisionTreeNode root;
    private Action actionNew;
    private Action actionOld;

    void Update()
    {
        actionNew.activated = false;        
        actionOld = actionNew;
        actionNew = root.MakeDecision() as Action;
        if (actionNew == null)
            actionNew = actionOld;
        actionNew.activated = true;
    }

    public override DecisionTreeNode MakeDecision()
    {
        return root.MakeDecision();
    }
}
