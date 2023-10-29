using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PositionRace : MonoBehaviour
{
    //0: Laps, 1: WaypointsPassedInLap; Laps*WaypointsPassedInLap=WaypoointsPassed
    public string playerName;
    public int WaypointsPassed;
    public float DistanceToReachWaypoint;
    bool doneLap=false;
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

        if(this.gameObject.name=="PlayerCar" && (WaypointsPassed % (CarreraManager.Instance.totalWaypointsInTrack/CarreraManager.Instance.numberOfLaps))==0){
            CarreraManager.Instance.currentLap++;
            if (CarreraManager.Instance.currentLap <= CarreraManager.Instance.numberOfLaps)
                CamaraGlobal.Instance.attachedCanvas.carUI.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                CarreraManager.Instance.currentLap + "/" + CarreraManager.Instance.numberOfLaps;
            else if (!CarreraManager.Instance.canWinRace){
                /* Codigo de diego pa mensajes  */
                if (GameObject.Find("TriggerRace") != null)
                {
                    GameObject.Find("TriggerRace").GetComponent<TriggerStoryEvent>().TriggerEvent();
                }
            CamaraGlobal.Instance.attachedCanvas.carUI.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=
             " ̵̢̛͈̦͉̦͈̘͇͈̱̱̖̅̉̃͐͗̎̏̌͒͐̅̀̐͛̐̌̀̏͆̅͆͆͗̉͘͘̚͜ ̷̨̧̢̡̮̞͉̳̺̜̭̣̹̝̹̘̫̬͕̗̝̟͉̙̥̦̈́̃̊͑̽́́̒̊̎͑̑̀̐͒́̋͆̐̑̋̕͜ͅ"+"/"+CarreraManager.Instance.numberOfLaps;
            }

        }
    }

    
}
