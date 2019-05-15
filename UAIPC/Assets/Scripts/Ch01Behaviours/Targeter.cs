using UnityEngine;
using System.Collections;

public class Targeter : MonoBehaviour
{
    public virtual Goal GetGoal()
    {
        return new Goal();
    }
}
