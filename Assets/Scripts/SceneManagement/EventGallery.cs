using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EventGallery : MonoBehaviour
{
    public static EventGallery Instance;
    public int neededWaypoint;

    [Header("Variables Necesarias")]
    [SerializeField] Tile downArrowTile;
    void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
    
    void Update(){
        if(GameObject.FindObjectOfType<CarController>()!=null && (neededWaypoint==GameObject.FindObjectOfType<CarController>().GetComponent<PositionRace>().WaypointsPassed || neededWaypoint==-1)){
            EventManager.Instance?.InvokeEvent();
            neededWaypoint=-2;
            
        }
    }

    //Primarios
    public void StartRace(){

    }

    //Stage 1
    public void GlitchStage1(){
        ActivateGlitchMap();
        CarreraManager.Instance.SetReversedPlayer();
        CarreraManager.Instance.NextStage="Nivel 4";
        CarreraManager.Instance.newPositionPlayer=Vector3.zero; //Por determinar

        CamaraGlobal.Instance.cameraFX.shdr_crt.scanlineIntensity=37.5f;
        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=-0.04f;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesX=450;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesY=450;

        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("crt",false,false,true,0.5f,0),
                new TransitionData("vram",false,false,true,0.5f,0)
            }
        );

        EventManager.Instance.eventAction-=GlitchStage1;
    }

    //Stage 2
    public void GlitchStage2(){
        ActivateGlitchMap();
        CarreraManager.Instance.SetReversedPlayer();
        CarreraManager.Instance.NextStage="Nivel 5";
        if(GameObject.Find("Lego Barrier")){
            Destroy(GameObject.Find("Lego Barrier"));
        }
        if(GameObject.Find("PencilGlitched2")){
            GameObject.Find("PencilGlitched2").GetComponent<PolygonCollider2D>().enabled=true;
            GameObject.Find("PencilGlitched2").GetComponent<AlterColorShaderScript>().enabled=false;
        }

        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesX=350;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesY=350;
        CamaraGlobal.Instance.cameraFX.shdr_unsync.speed=10.05f;
        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=-0.08f;
        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("bc",false,false,true,0f,0f),
                new TransitionData("vram",false,false,true,0f,0f),
                new TransitionData("unsync",false,false,true,0.5f,0f)
            }
        );
        
        EventManager.Instance.eventAction-=GlitchStage2;
    }
    //Stage 3
    public void GlitchPlayer(){
        CanWinRace(false);

        CarreraManager.Instance.NormalTilemap.SetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(new Vector2(-14,3)),downArrowTile);
        CarreraManager.Instance.NormalTilemap.SetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(new Vector2(-13,1)),downArrowTile);
        CarreraManager.Instance.NormalTilemap.SetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(new Vector2(-14,1)),downArrowTile);

        CarreraManager.Instance?.SetGlitchPlayer();
        
        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("bc",false,true,true,0.5f,0)
            }
        );
        EventManager.Instance.eventAction-=GlitchPlayer;
    }
    public void GlitchStage3(){
        ActivateGlitchMap();
        CarreraManager.Instance.SetReversedPlayer();

        CamaraGlobal.Instance.cameraFX.shdr_unsync.speed=10.55f;

        CarreraManager.Instance.NextStage="Nivel 5";
        DontDestroyOnLoad(GameObject.Find("PlayerCar"));
        if(GameObject.Find("Pencil_Barrier")){
            GameObject.Find("Pencil_Barrier").layer=LayerMask.NameToLayer("Obstacles");
        }

        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=0.55f;

        EventManager.Instance.eventAction-=GlitchStage3;
    }
    

    //Secundarios
    void ActivateGlitchMap(){
        Debug.LogWarning("1dd");
        if(CarreraManager.Instance!=null)
        CarreraManager.Instance.GlitchedTilemap.GetComponent<TilemapRenderer>().enabled=true;
    }

    void CanWinRace(bool b){
        CarreraManager.Instance?.SetCanWin(b);
    }
}