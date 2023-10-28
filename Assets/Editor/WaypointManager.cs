using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointManager : EditorWindow
{
    // :

    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManager>();
    }

    public Transform WaypointRoot;
    public GameObject WaypointPrefab;
    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("WaypointRoot"));
        EditorGUILayout.PropertyField(obj.FindProperty("WaypointPrefab"));

        if (WaypointRoot == null) EditorGUILayout.HelpBox("Debes seleccionar un WaypointRoot", MessageType.Warning);
        else
        {
            EditorGUILayout.BeginVertical("box");
            /*
            DrawButtons();
            EditorGUILayout.EndVertical();
            */
            
            try
            {
                DrawButtons();
            }
            finally
            {
                EditorGUILayout.EndVertical();
            }
            
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Crear Waypoint Suelto"))
        {
            CreateNewWaypoint();
        }
        if(WaypointRoot.childCount>0) 
        {
            
            if(GUILayout.Button("Crear Waypoint Antes"))
            {
                CreatePrevWaypoint();
            }
            if (GUILayout.Button("Crear Waypoint Despues"))
            {
                CreateNextWaypoint();
            }

            /*EditorGUILayout.LabelField("Opciones de Waypoints sueltos",EditorStyles.boldLabel);
            if(GUILayout.Button("Crear Waypoint Antes Suelto"))
            {
                CreateWaypointBefore();
            }
            if (GUILayout.Button("Crear Waypoint Despues Suelto"))
            {
                CreateWaypointAfter();
            }*/


            if (GUILayout.Button("Eliminar Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    void CreateNewWaypoint()
    {
        GameObject waypointObject = Instantiate(WaypointPrefab);
        waypointObject.name="Waypoint"+WaypointRoot.childCount;
        waypointObject.transform.SetParent(WaypointRoot, false);

        Vector2 pos=SceneView.lastActiveSceneView.camera.transform.position;
        waypointObject.transform.position=pos;
    }

    void RemoveWaypoint(){
        Waypoint selectedWaypoint=Selection.activeGameObject.GetComponent<Waypoint>();
        if(selectedWaypoint==null){return;}

        foreach(Waypoint wp in selectedWaypoint.FollowingWaypoints){
            wp.PreviousWaypoints.Remove(selectedWaypoint);
        }
        foreach(Waypoint wp in selectedWaypoint.PreviousWaypoints){
            wp.FollowingWaypoints.Remove(selectedWaypoint);
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void CreateNextWaypoint(){
        Waypoint selectedWaypoint=Selection.activeGameObject.GetComponent<Waypoint>();
        if(selectedWaypoint==null){return;}

        GameObject waypointObject = Instantiate(WaypointPrefab);
        waypointObject.name="Waypoint"+WaypointRoot.childCount;
        waypointObject.transform.SetParent(WaypointRoot, false);

        Waypoint waypoint=waypointObject.GetComponent<Waypoint>();
        foreach(Waypoint wp in selectedWaypoint.FollowingWaypoints){
            wp.PreviousWaypoints.Add(waypoint);
            wp.PreviousWaypoints.Remove(selectedWaypoint);
            waypoint.FollowingWaypoints.Add(wp);
        }

        selectedWaypoint.FollowingWaypoints.Clear();
        selectedWaypoint.FollowingWaypoints.Add(waypoint);
        waypoint.PreviousWaypoints.Add(selectedWaypoint);
        waypoint.transform.position=selectedWaypoint.transform.position;
        waypoint.transform.forward=selectedWaypoint.transform.forward;

        Selection.activeObject=waypoint.gameObject;
    }

    void CreatePrevWaypoint(){
        Waypoint selectedWaypoint=Selection.activeGameObject.GetComponent<Waypoint>();
        if(selectedWaypoint==null){return;}
        
        GameObject waypointObject = Instantiate(WaypointPrefab);
        waypointObject.name="Waypoint"+WaypointRoot.childCount;
        waypointObject.transform.SetParent(WaypointRoot, false);

        Waypoint waypoint=waypointObject.GetComponent<Waypoint>();
        foreach(Waypoint wp in selectedWaypoint.FollowingWaypoints){
            wp.FollowingWaypoints.Add(waypoint);
            wp.FollowingWaypoints.Remove(selectedWaypoint);
            waypoint.PreviousWaypoints.Add(wp);
        }

        selectedWaypoint.PreviousWaypoints.Clear();
        selectedWaypoint.PreviousWaypoints.Add(waypoint);
        waypoint.FollowingWaypoints.Add(selectedWaypoint);
        waypoint.transform.position=selectedWaypoint.transform.position;
        waypoint.transform.forward=selectedWaypoint.transform.forward;

        Selection.activeObject=waypoint.gameObject;
    }
}
