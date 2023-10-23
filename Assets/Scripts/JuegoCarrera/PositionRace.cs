using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionRace : MonoBehaviour
{
    //0: Laps, 1: WaypointsPassedInLap; Laps*WaypointsPassedInLap=WaypoointsPassed
    public string playerName;
    public int WaypointsPassed;
    public float DistanceToReachWaypoint;
    //public bool hasEnded=false;
    [HideInInspector] public Sprite spriteUI;

    void Update(){
        /*if(hasEnded){
            hasEnded=false;
            CarreraManager.Instance.RaceFinished(this.gameObject.name,this);
        }*/
    }
    public void PassedWaypoint(){
        
        if(WaypointsPassed<CarreraManager.Instance.totalWaypointsInTrack)
        WaypointsPassed++;


        if(WaypointsPassed>=CarreraManager.Instance.totalWaypointsInTrack && CarreraManager.Instance.allPositions.Contains(this)){
            CarreraManager.Instance.RaceFinished(this.gameObject.name,this);
        }
    }

    
}
