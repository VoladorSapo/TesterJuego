using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NPCWaypoint
{
    public int[] NextWaypointsInList;
    public Vector2 pos;

    public int PickRandom(){
        if(NextWaypointsInList.Count()<=0){return -1;}
        int rIndex=UnityEngine.Random.Range(0,NextWaypointsInList.Count());
        return NextWaypointsInList[rIndex];
    }
}