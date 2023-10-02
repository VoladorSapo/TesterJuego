using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;

public class Waypoint : MonoBehaviour
{
    public bool _carSlowsDown;
    public float _distanceToReachWaypoint=1;

    [Range(0f,1f)] public float WPweight=1;
    public List<Waypoint> FollowingWaypoints=new List<Waypoint>();
    private List<Waypoint> followWP=new List<Waypoint>();
    public List<Waypoint> PreviousWaypoints=new List<Waypoint>();
    private List<Waypoint> prevWP=new List<Waypoint>();

    /*private void OnValidate(){
        if(!FollowingWaypoints.SequenceEqual(followWP)){
            List<Waypoint> difList=FollowingWaypoints.Except(followWP).ToList();
            foreach(Waypoint wp in difList){
                if(this!=null && wp!=null)
                wp.PreviousWaypoints.Add(this);
            }
            Clone(followWP,FollowingWaypoints);
        }

        if(!PreviousWaypoints.SequenceEqual(prevWP)){
            List<Waypoint> difList=PreviousWaypoints.Except(prevWP).ToList();
            foreach(Waypoint wp in difList){
                if(this!=null && wp!=null)
                wp.FollowingWaypoints.Add(this);
            }
            Clone(prevWP,PreviousWaypoints);
        }
        
    }*/


    private void Clone<T>(List<T> list1, List<T> lis2){
        list1.Clear();
        for(int i=0; i<lis2.Count; i++){
            list1.Add(lis2[i]);
        }
    }
}
