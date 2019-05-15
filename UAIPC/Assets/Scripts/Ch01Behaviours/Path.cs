using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

    public List<GameObject> nodes;
    List<PathSegment> segments;


    void Start() {
        segments = GetSegments();
    }

    void OnDrawGizmos () {
        Vector3 direction;
        Color tmp = Gizmos.color;
        Gizmos.color = Color.magenta;
        int i;
        for (i = 0; i < nodes.Count - 1; i++)
        {
            Vector3 src = nodes[i].transform.position;
            Vector3 dst = nodes[i+1].transform.position;
            direction = dst - src;
            Gizmos.DrawRay(src, direction);
        }
        Gizmos.color = tmp;
    }

    private PathSegment GetNearestSegment (Vector3 position) {
        float nearestDistance = Mathf.Infinity;
        float distance = nearestDistance;
        Vector3 centroid = Vector3.zero;
        PathSegment segment = new PathSegment();
        foreach (PathSegment s in segments)
        {
            centroid = (s.a + s.b) / 2.0f;
            distance = Vector3.Distance(position, centroid);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                segment = s;
            }
        }
        return segment;
    }

    public List<PathSegment> GetSegments () {
        List<PathSegment> segments = new List<PathSegment>();
        int i;
        for (i = 0; i < nodes.Count - 1; i++)
        {
            Vector3 src = nodes[i].transform.position;
            Vector3 dst = nodes[i+1].transform.position;
            PathSegment segment = new PathSegment(src, dst);
            segments.Add(segment);
        }
        return segments;
    }

    public float GetParam(Vector3 position, float lastParam) {
        float param = 0f;

        PathSegment currentSegment = null;
        float tempParam = 0f;

        //We find the current segment in the path where the agent is
        foreach (PathSegment ps in segments)
        {
            tempParam += Vector3.Distance(ps.a, ps.b);

            if (lastParam <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }

        if (currentSegment == null)
            return 0f;

        Vector3 segmentDirection = currentSegment.b - currentSegment.a;
        segmentDirection.Normalize();
        Vector3 currPos = position - currentSegment.a;
        //We use vector projections to find the point over the segment
        Vector3 pointInSegment = Vector3.Project(currPos, segmentDirection);
        //The current param is the sum of distances until the last node
        //plus the length of the projection from last step
        param = tempParam - Vector3.Distance(currentSegment.a, currentSegment.b);
        param += pointInSegment.magnitude;
        return param;
    }

    public Vector3 GetPosition(float param) 
    {
        Vector3 position = Vector3.zero;

        PathSegment currentSegment = null;
        float tempParam = 0f;

        //We find the current segment in the path where the agent is
        foreach (PathSegment ps in segments)
        {
            tempParam += Vector3.Distance(ps.a, ps.b);
            if (param <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }

        if (currentSegment == null)
            return Vector3.zero;

        Vector3 segmentDirection = currentSegment.b - currentSegment.a;
        segmentDirection.Normalize();
        tempParam -= Vector3.Distance(currentSegment.a, currentSegment.b);
        tempParam = param - tempParam;
        position = currentSegment.a + segmentDirection * tempParam;
        return position;
    }
}
