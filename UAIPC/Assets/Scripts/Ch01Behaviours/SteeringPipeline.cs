using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteeringPipeline : Wander
{
    public int constraintSteps = 3;
    Targeter[] targeters;
    Decomposer[] decomposers;
    Constraint[] constraints;
    Actuator actuator;

    void Start ()
    {
        targeters = GetComponents<Targeter>();
        decomposers = GetComponents<Decomposer>();
        constraints = GetComponents<Constraint>();
        actuator = GetComponent<Actuator>();
    }

    public override Steering GetSteering()
    {
        Goal goal = new Goal();
        foreach (Targeter targeter in targeters)
            goal.UpdateChannels(targeter.GetGoal());
        foreach (Decomposer decomposer in decomposers)
            goal = decomposer.Decompose(goal);
        for (int i = 0; i < constraintSteps; i++)
        {
            Path path = actuator.GetPath(goal);
            foreach (Constraint constraint in constraints)
            {
                if (constraint.WillViolate(path))
                {
                    goal = constraint.Suggest(path);
                    break;
                }
                return actuator.GetOutput(path, goal);
            }
        }
        return base.GetSteering();
    }
}