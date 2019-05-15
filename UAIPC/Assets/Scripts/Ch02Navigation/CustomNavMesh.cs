using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomNavMesh : Graph
{
    public override void Start()
    {
        instIdToId = new Dictionary<int, int>();
    }
}
