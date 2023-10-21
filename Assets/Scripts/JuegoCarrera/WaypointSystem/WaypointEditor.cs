using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
public class WaypointEditor
{
   [DrawGizmo(GizmoType.NotInSelectionHierarchy| GizmoType.Selected | GizmoType.Pickable)] 
   public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType){
    if(waypoint!=null){
        GameObject selectecWP=Selection.activeGameObject;
        if(selectecWP!=null && waypoint.gameObject.name==selectecWP.name){
            Gizmos.color=Color.yellow;
            if(waypoint.isCheckpoint){
                Gizmos.color=Color.green*1.5f;
            }
        }else{
            Gizmos.color=Color.yellow*0.5f;
            if(waypoint.isCheckpoint){
                Gizmos.color=Color.green*0.5f;
            }
        }
        
        Gizmos.DrawSphere(waypoint.transform.position, 0.5f);

        Vector2 up=waypoint.transform.up*5f;
        Gizmos.color=Color.blue;
        Gizmos.DrawLine(new Vector3(-up.y,up.x,0)+waypoint.transform.position,new Vector3(up.y,-up.x,0)+waypoint.transform.position);

        if(waypoint.FollowingWaypoints!=null){
            foreach(Waypoint wp in waypoint.FollowingWaypoints){
                Gizmos.color=Color.red;
                if(wp!=null)
                DrawArrow(waypoint.transform.position, wp.transform.position,1f,0.5f);
            }
        }

        if(waypoint.FollowingCheckpoints!=null){
            foreach(Waypoint wp in waypoint.FollowingCheckpoints){
                Gizmos.color=Color.red;
                if(wp!=null)
                DrawArrow(waypoint.transform.position, wp.transform.position,1f,0.5f);
            }
        }
    }

    
   }

   public static void DrawArrow(Vector2 startPos, Vector2 headPos, float distanceHead, float arrowWidth){
        Vector2 dir=headPos-startPos;
        float mag=dir.magnitude;

        Vector2 auxPoint=startPos+dir.normalized*(mag-distanceHead);
        Vector2 auxVector=dir.normalized*arrowWidth;
        Vector2 headVector=new Vector2(-auxVector.y,auxVector.x);

        Vector2 point1=auxPoint+headVector;
        Vector2 point2=auxPoint-headVector;

        Gizmos.DrawLine(startPos,headPos);
        Gizmos.DrawLine(headPos,point1);
        Gizmos.DrawLine(headPos,point2);
   }

   
}
