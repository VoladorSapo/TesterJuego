using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;

public class Waypoint : MonoBehaviour
{
    public bool _carSlowsDown;
    public float _distanceToReachWaypoint=5;
    public LayerMask carMask;

    [Range(0f,1f)] public float WPweight=1;
    public List<Waypoint> FollowingWaypoints=new List<Waypoint>();
    //private List<Waypoint> followWP=new List<Waypoint>();
    public List<Waypoint> PreviousWaypoints=new List<Waypoint>();
    //private List<Waypoint> prevWP=new List<Waypoint>();

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

    void OnTriggerEnter2D(Collider2D col){
        //Debug.Log("ja");
        CarAI carAI=col.gameObject.GetComponent<CarAI>();
        CarController carPlayer=col.gameObject.GetComponent<CarController>();
        if((carMask.value & (1 << col.gameObject.layer)) != 0){

            if(carPlayer!=null && carPlayer.CurrentWaypoint!=null && carPlayer.CurrentWaypoint.name==this.gameObject.name){
                carPlayer.gameObject.GetComponent<PositionRace>().DistanceToReachWaypoint=
                Vector3.Distance(new Vector3(carPlayer.transform.position.x,carPlayer.transform.position.y,0),
                new Vector3(FollowingWaypoints[0].transform.position.x,FollowingWaypoints[0].transform.position.y,0));

                carPlayer.gameObject.GetComponent<PositionRace>().PassedWaypoint();
                carPlayer.CurrentWaypoint=FollowingWaypoints[0];
            }

            if(carAI!=null && carAI.CurrentWaypoint!=null && carAI.CurrentWaypoint.name==this.gameObject.name){
                carAI.gameObject.GetComponent<PositionRace>().DistanceToReachWaypoint=
                Vector3.Distance(new Vector3(carAI.transform.position.x,carAI.transform.position.y,0),
                new Vector3(FollowingWaypoints[0].transform.position.x,FollowingWaypoints[0].transform.position.y,0));

                carAI.gameObject.GetComponent<PositionRace>().PassedWaypoint();
                carAI.CurrentWaypoint=FollowingWaypoints[0];
                carAI.PreviousWaypoint=this;
            }
        }
    }
}
