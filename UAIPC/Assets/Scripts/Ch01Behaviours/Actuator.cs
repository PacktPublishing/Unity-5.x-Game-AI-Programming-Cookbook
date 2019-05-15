using UnityEngine;
using System.Collections;

public class Actuator : MonoBehaviour
{
    public virtual Path GetPath (Goal goal)
    {
        return new Path();
    }

    public virtual Steering GetOutput (Path path, Goal goal)
    {
        return new Steering();
    }
}
