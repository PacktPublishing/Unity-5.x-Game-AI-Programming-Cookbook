using UnityEngine;
using System.Collections.Generic;

public class GraphDirichlet : Graph
{
    Dictionary<int, List<int>> objToVertex;

    public void AddLocation(VertexReport report)
    {
        int objId = report.obj.GetInstanceID();
        if (!objToVertex.ContainsKey(objId))
        {
            objToVertex.Add(objId, new List<int>());
        }
        objToVertex[objId].Add(report.vertex);
    }

    public void RemoveLocation(VertexReport report)
    {
        int objId = report.obj.GetInstanceID();
        objToVertex[objId].Remove(report.vertex);
    }

    public override void Start()
    {
        base.Start();
        objToVertex = new Dictionary<int, List<int>>();
    }

    public override void Load()
    {
        Vertex[] verts = GameObject.FindObjectsOfType<Vertex>();
        vertices = new List<Vertex>(verts);
        for (int i = 0; i < vertices.Count; i++)
        {
            VertexVisibility vv = vertices[i] as VertexVisibility;
            vv.id = i;
            vv.FindNeighbours(vertices);
        }
    }

    public override Vertex GetNearestVertex(Vector3 position)
    {
        Vertex vertex = null;
        float dist = Mathf.Infinity;
        float distNear = dist;
        Vector3 posVertex = Vector3.zero;
        for (int i = 0; i < vertices.Count; i++)
        {
            posVertex = vertices[i].transform.position;
            dist = Vector3.Distance(position, posVertex);
            if (dist < distNear)
            {
                distNear = dist;
                vertex = vertices[i];
            }
        }
        return vertex;
    }

    public Vertex GetNearestVertex(GameObject obj)
    {
        int objId = obj.GetInstanceID();
        Vector3 objPos = obj.transform.position;
        if (!objToVertex.ContainsKey(objId))
            return null;
        List<int> vertIds = objToVertex[objId];
        Vertex vertex = null;
        float dist = Mathf.Infinity;
        for (int i = 0; i < vertIds.Count; i++)
        {
            int id = vertIds[i];
            Vertex v = vertices[id];
            Vector3 vPos = v.transform.position;
            float d = Vector3.Distance(objPos, vPos);
            if (d < dist)
            {
                vertex = v;
                dist = d;
            }
        }
        return vertex;
    }

    public override Vertex[] GetNeighbours(Vertex v)
    {
        List<Edge> edges = v.neighbours;
        Vertex[] ns = new Vertex[edges.Count];
        int i;
        for (i = 0; i < edges.Count; i++)
        {
            ns[i] = edges[i].vertex;
        }
        return ns;
    }

    public override Edge[] GetEdges(Vertex v)
    {
        return vertices[v.id].neighbours.ToArray();
    }


}
