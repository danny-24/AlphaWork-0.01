using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[CustomEditor(typeof(AIWaypointNetwork))]
public class AIWaypointNetworkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;
        if(network.pathDisplayMode == PathDisplayMode.NextPath)
        {
            network.UIStart = EditorGUILayout.IntSlider("Start", network.UIStart, 0, network.waypoints.Count - 1);
            network.UIEnd = EditorGUILayout.IntSlider("End", network.UIEnd, 0, network.waypoints.Count - 1);
        }

        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        for (int i = 0; i < network.waypoints.Count; i++)
        {
            Handles.color = Color.blue;
            if(network.waypoints[i] != null)
            Handles.Label(network.waypoints[i].transform.position, "WayPoint " + i.ToString());
        }

        if(network.pathDisplayMode == PathDisplayMode.Connections)
        {
            Vector3[] linePoints = new Vector3[network.waypoints.Count + 1];

            for (int i = 0; i <=network.waypoints.Count; i++)
            {
                int index = i != network.waypoints.Count ? i : 0;

                if (network.waypoints[index] != null)
                    linePoints[i] = network.waypoints[index].transform.position;
                else
                    linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);                
            }

            Handles.color = Color.blue;
            Handles.DrawPolyLine(linePoints);
        }
        else if (network.pathDisplayMode == PathDisplayMode.NextPath)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 from = network.waypoints[network.UIStart].position;
            Vector3 to = network.waypoints[network.UIEnd].position;

            NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);

            Handles.color = Color.green;
            Handles.DrawPolyLine(path.corners);
        }
        else if(network.pathDisplayMode == PathDisplayMode.AllPaths)
        {

        }
    }
}
