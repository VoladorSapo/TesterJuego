using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarreraManager : MonoBehaviour
{
    public static CarreraManager Instance;
    Transform WaypointRoot;
    public int totalWaypointsInTrack=10;
    public List<PositionRace> allPositions;
    void Awake()
    {
        if(Instance==null){
            Instance=this;
        }else{
            Destroy(this);
        }

        CarAI[] allAICarsInScene=GameObject.FindGameObjectsWithTag("AICar").Select(car=>car.GetComponent<CarAI>()).Where(ms=>ms!=null).ToArray();
        foreach(CarAI ai in allAICarsInScene){
            ai.CurrentWaypoint=GameObject.Find("Waypoint0").GetComponent<Waypoint>();
            ai.PreviousWaypoint=ai.CurrentWaypoint.PreviousWaypoints[0];
        }

        WaypointRoot=GameObject.Find("WaypointRoot").transform;

        allPositions=GameObject.FindObjectsOfType<PositionRace>().ToList();
        foreach(PositionRace player in allPositions){
            player.WaypointsPassed=0;
            player.playerName=player.gameObject.name;
        }
    }

    public void setNumberOfWaypoints(int lapsInTrack){
        totalWaypointsInTrack=lapsInTrack*WaypointRoot.childCount;
    }

    public void OrderPositionsList(){
        allPositions=allPositions.OrderByDescending(wp=>wp.WaypointsPassed).ToList();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.S)){
            foreach(PositionRace pos in allPositions){
                Debug.Log(pos.playerName);
            }
        }
    }

    
}
