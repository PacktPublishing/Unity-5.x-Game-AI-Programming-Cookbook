using UnityEngine;

public class VertexDirichlet : Vertex
{
    public void OnTriggerEnter(Collider col)
    {
        string objName = col.gameObject.name;
        if (objName.Equals("Agent") || objName.Equals("Player"))
        {
            VertexReport report = new VertexReport(id, col.gameObject);
            SendMessageUpwards("AddLocation", report);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        string objName = col.gameObject.name;
        if (objName.Equals("Agent") || objName.Equals("Player"))
        {
            VertexReport report = new VertexReport(id, col.gameObject);
            SendMessageUpwards("RemoveLocation", report);
        }
    }
}
