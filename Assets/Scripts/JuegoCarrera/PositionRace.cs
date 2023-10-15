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

    public void PassedWaypoint(){
        
        if(WaypointsPassed<CarreraManager.Instance.totalWaypointsInTrack)
        WaypointsPassed++;

        CarreraManager.Instance.OrderPositionsList();

        if(WaypointsPassed>=CarreraManager.Instance.totalWaypointsInTrack){
            Debug.Log(playerName+" is the winner");
        }
    }

    
}
