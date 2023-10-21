using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTilemap : MonoBehaviour
{
    [SerializeField] PositionRace playerPositionInRace;
    bool enabledGlitchedMap=false;
    void Start(){
        if(CarreraManager.Instance.GlitchedTilemap!=null)
        CarreraManager.Instance.GlitchedTilemap.gameObject.GetComponent<TilemapRenderer>().enabled=false;
    }
    void Update(){
        /*if(playerPositionInRace!=null && playerPositionInRace.WaypointsPassed>=17 && !enabledGlitchedMap){
            Debug.Log("cambio!!");
            enabledGlitchedMap=true;
            GlitchTiles();
        }

        if(Input.GetKeyDown(KeyCode.K)){
            enabledGlitchedMap=true;
            GlitchTiles();
        }*/
    }

    void GlitchTiles(){
        if(CarreraManager.Instance.GlitchedTilemap!=null)
        CarreraManager.Instance.GlitchedTilemap.gameObject.GetComponent<TilemapRenderer>().enabled=true;
        SceneManagement.Instance.ApplyTransitionEffect(new TransitionData("bc",true,true,true,5f));
    }
}
