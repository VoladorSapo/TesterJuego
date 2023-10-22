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
    [HideInInspector] public Sprite spriteUI;

    public void PassedWaypoint(){
        
        if(WaypointsPassed<CarreraManager.Instance.totalWaypointsInTrack)
        WaypointsPassed++;

        CarreraManager.Instance.OrderPositionsList();

        if(WaypointsPassed>=CarreraManager.Instance.totalWaypointsInTrack){
            CarreraManager.Instance.RaceFinished(this.gameObject.name);
        }
    }

    
}
