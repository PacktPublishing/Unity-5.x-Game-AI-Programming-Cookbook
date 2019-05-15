using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CustomNavMeshWindow : EditorWindow
{
    public static bool isEnabled = false;
    private static GameObject graphVertex;
    public static GameObject graphObj;
    public static CustomNavMesh graph;
    static CustomNavMeshWindow window;

    //[MenuItem("UAIPC/Ch02/CustomNavMeshWindow")]
    static void Init()
    {
        //window = EditorWindow.GetWindow<CustomNavMeshWindow>();
        //window.title = "CustomNavMeshWindow";

        SceneView.onSceneGUIDelegate += OnScene;
        graphObj = GameObject.Find("CustomNavMesh");
        if (graphObj == null)
        {
            graphObj = new GameObject("CustomNavMesh");
            graphObj.AddComponent<CustomNavMesh>();
            graph = graphObj.GetComponent<CustomNavMesh>();
        }
        else
        {
            graph = graphObj.GetComponent<CustomNavMesh>();
            if (graph == null)
                graphObj.AddComponent<CustomNavMesh>();
            graph = graphObj.GetComponent<CustomNavMesh>();
        }
    }

    private static void OnScene(SceneView sceneView)
    {
        if (!isEnabled)
            return;
        if (Event.current.type == EventType.MouseDown)
        {
            graphVertex = graph.vertexPrefab;
            if (graphVertex == null)
            {
                Debug.LogError("No Vertex Prefab assigned");
                return;
            }
            Event e = Event.current;
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            RaycastHit hit;
            GameObject newV;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                Mesh mesh = obj.GetComponent<MeshFilter>().sharedMesh;
                Vector3 pos;
                int i;
                for (i = 0; i < mesh.triangles.Length; i += 3)
                {
                    int i0 = mesh.triangles[i];
                    int i1 = mesh.triangles[i + 1];
                    int i2 = mesh.triangles[i + 2];
                    pos = mesh.vertices[i0];
                    pos += mesh.vertices[i1];
                    pos += mesh.vertices[i2];
                    pos /= 3;
                    newV = (GameObject)Instantiate(graphVertex, pos, Quaternion.identity);
                    newV.transform.Translate(obj.transform.position);
                    newV.transform.parent = graphObj.transform;
                    graphObj.transform.parent = obj.transform;
                }
            }
        }       
    }

    void OnGUI()
    {
        isEnabled = EditorGUILayout.Toggle("Enable Mesh Pikcing", isEnabled);
        if (GUILayout.Button("Build Edges"))
        {
            if (graph != null)
                graph.Load();
        }
    }

    void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= OnScene;
    }
}
