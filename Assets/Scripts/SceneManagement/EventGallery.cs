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
    [SerializeField] Tile[] windowTiles;
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

        if(Input.GetKey(KeyCode.B)){
            GlitchWindow();
        }
    }

    //Primarios
    public void StartRace(){

    }

    //Stage 1
    public void GlitchStage1(){
        ActivateGlitchMap();
        CarreraManager.Instance.SetReversedPlayer();
        CarreraManager.Instance.NextStage="ConversacionIntermediaCoche1";
        CarreraManager.Instance.totalWaypointsInTrack=27*3;
        CarreraManager.Instance.newPositionPlayer=Vector3.zero; //Por determinar

        CamaraGlobal.Instance.cameraFX.shdr_crt.scanlineIntensity=22.5f;
        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=-0.04f;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesX=450;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesY=450;

        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("crt",false,false,true,0.5f,0),
                new TransitionData("vram",false,false,true,0.5f,0),
                new TransitionData("mos",false,false,true,0.5f,0)
            }
        );

        SceneManagement.Instance.globalChange=2;
        EventManager.Instance.eventAction-=GlitchStage1;
    }

    //Stage 2
    public void GlitchStage2(){
        ActivateGlitchMap();
        CarreraManager.Instance.SetReversedPlayer();
        CarreraManager.Instance.totalWaypointsInTrack=32*3;

        CarreraManager.Instance.NextStage="ConversacionIntermediaCoche2";
        if(GameObject.Find("Lego Barrier")){
            Destroy(GameObject.Find("Lego Barrier"));
        }
        if(GameObject.Find("PencilGlitched2")){
            GameObject.Find("PencilGlitched2").GetComponent<PolygonCollider2D>().enabled=true;
            GameObject.Find("PencilGlitched2").GetComponent<AlterColorShaderScript>().enabled=false;
        }

        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesX=350;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesY=350;
        CamaraGlobal.Instance.cameraFX.shdr_unsync.speed=10.35f;
        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=-0.08f;
        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("bc",false,false,true,0f,0f),
                new TransitionData("crt",false,false,true,0.5f,0),
                new TransitionData("vram",false,false,true,0f,0f),
                new TransitionData("unsync",false,false,true,0.5f,0f),
                new TransitionData("mos",false,false,true,0.5f,0)
            }
        );
        
        SceneManagement.Instance.globalChange=2;
        EventManager.Instance.eventAction-=GlitchStage2;
    }
    //Stage 3
    public void GlitchPlayer(){
        CanWinRace(false);

        CarreraManager.Instance.NormalTilemap.SetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(new Vector2(-14,3)),downArrowTile);
        CarreraManager.Instance.NormalTilemap.SetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(new Vector2(-13,1)),downArrowTile);
        CarreraManager.Instance.NormalTilemap.SetTile(CarreraManager.Instance.NormalTilemap.WorldToCell(new Vector2(-14,1)),downArrowTile);

        CarreraManager.Instance?.SetGlitchPlayer();

        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=-0.08f;
        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("bc",false,true,true,0.5f,0f),
                new TransitionData("vram",false,true,true,0.5f,0f)
            }
        );
        
        StartCoroutine(SetVram());

        SceneManagement.Instance.globalChange=2;
        EventManager.Instance.eventAction-=GlitchPlayer;

    }
    IEnumerator SetVram(){
        yield return new WaitForSeconds(1f);
        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=-2f;
    }
    public void GlitchStage3(){
        ActivateGlitchMap();
        CarreraManager.Instance.SetReversedPlayer();
        CarreraManager.Instance.totalWaypointsInTrack=31*3;

        CamaraGlobal.Instance.cameraFX.shdr_unsync.speed=10.65f;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesX=300;
        CamaraGlobal.Instance.cameraFX.shdr_mos._numberOfTilesY=300;
        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=-0.12f;

        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("bc",false,false,true,0f,0f),
                new TransitionData("crt",false,false,true,0.5f,0),
                new TransitionData("vram",false,false,true,0f,0f),
                new TransitionData("unsync",false,false,true,0.5f,0f),
                new TransitionData("mos",false,false,true,0.5f,0),
                new TransitionData("invc",false,false,true,0f,0.5f)
            }
        );

        CarreraManager.Instance.NextStage="Nivel 5";
        DontDestroyOnLoad(GameObject.Find("PlayerCar"));
        if(GameObject.Find("Pencil_Barrier")){
            GameObject.Find("Pencil_Barrier").layer=LayerMask.NameToLayer("Obstacles");
        }

        CamaraGlobal.Instance.cameraFX.shdr_vram.shift=0.55f;

        SceneManagement.Instance.globalChange=0;
        SceneManagement.Instance.narrativeParts.PlatformNarrative=2;
        EventManager.Instance.eventAction-=GlitchStage3;
    }
    
    //Zelda Vidriera
    public void GlitchWindow(){
        Tilemap solidMap=GameObject.Find("Solid").GetComponent<Tilemap>();
        int n=0;

        for(int i=0; i<windowTiles.Length; i+=4){
            solidMap.SetTile(new Vector3Int(-13,14+n,0),windowTiles[i+0]);
            solidMap.SetTile(new Vector3Int(-12,14+n,0),windowTiles[i+1]);
            solidMap.SetTile(new Vector3Int(-11,14+n,0),windowTiles[i+2]);
            solidMap.SetTile(new Vector3Int(-10,14+n,0),windowTiles[i+3]);
            n++;
        }
        GameObject.Find("FinalWarpPoint").GetComponent<BoxCollider2D>().enabled=true;

        //Sonido
        
        
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
