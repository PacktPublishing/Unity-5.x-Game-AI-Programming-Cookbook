using UnityEngine;
using System.Collections;

public class Constraint : MonoBehaviour
{
	// Use this for initialization
	public virtual bool WillViolate (Path path)
    {
        return true;
    }

    public virtual Goal Suggest (Path path) {
        return new Goal();
    }
}
