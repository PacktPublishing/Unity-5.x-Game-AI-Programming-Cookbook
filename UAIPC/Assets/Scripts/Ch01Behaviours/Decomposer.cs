using UnityEngine;
using System.Collections;

public class Decomposer : MonoBehaviour
{
	public virtual Goal Decompose (Goal goal)
    {
        return goal;
    }
}
