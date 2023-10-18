using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    public int globalNarrativePart;
    private int globalPrevNarrativePart=0;

    public int localNarrativePart;
    private int localPrevNarrativePart=0;

    [Header("Escenas del Juego de Plataformas")]
    public List<string> allPlatformLevels;
    //...

    [Header("Escenas del Juego de Carreras")]
    public List<string> allStages;
    public List<string> menuScenes;

    [Header("Escenas del Juego Zelda")]
    public GameObject zeldaPrefab;
    [SerializeField] Vector3 posZeldaInicio;
    //...

    //Escenas de control
    Scene currentScene;
    Scene previousScene;

    //Para cambios de escenas
    //Camara Global
    CamaraGlobal camaraGlobal;


    void Awake(){
        if(SceneManagement.Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
    void Start(){
        currentScene=SceneManager.GetActiveScene();
        camaraGlobal=CamaraGlobal.Instance;
        StartSettings();
    }
    void Update()
    {   
        currentScene=SceneManager.GetActiveScene();

        if(Input.GetKeyDown(KeyCode.L)){
            GameObject.FindObjectOfType<GlobalWarpPoint>().DoTransition();
        }

        if(previousScene==null || previousScene.name!=currentScene.name){
            previousScene=currentScene;
            SceneChanges();
        }

        //Cambios Narrativos que no dependen del cambio de escenas
        if(localPrevNarrativePart!=localNarrativePart){
                localPrevNarrativePart=localNarrativePart;
                NarrativeChanges();
        }

        ConstantChanges();
    }

    void SpawnZeldaPlayer(){
        
        int numberOfPlayers=GameObject.FindGameObjectsWithTag("Player").Where(obj=>obj.GetComponent<PlayerMoveScript>()!=null).Count();
        if(numberOfPlayers==0){
            GameObject go=Instantiate(zeldaPrefab,posZeldaInicio,Quaternion.identity);
            go.name=zeldaPrefab.name;
        }
    }

    void SceneChanges(){

        CarSettings();
        SceneMusic();
        //Cambios Narrativos que dependen del cambio de escenas
        if(globalPrevNarrativePart!=globalNarrativePart){
                globalPrevNarrativePart=globalNarrativePart;
                switch(globalNarrativePart){
                    case 0: break;
                    case 1: break;
                    case 2: CarSettings(); CameraSettings(2,"PlayerCar",-1); AudioSettings("PlayerCar"); break;
                    case 3: break;
                    case 4: SpawnZeldaPlayer(); CameraSettings(2,"ZeldaPlayer",-1); AudioSettings("ZeldaPlayer"); break;
                    default: break; //Temporalmente está así
                }
        }
        
    }

    void NarrativeChanges(){
                switch(localNarrativePart){
                    case 0: break;
                    case 1: break;
                    case 2: break;
                    case 3: CarreraManager.Instance.EndRace(); break;
                    case 4: SpawnZeldaPlayer(); break;
                    default: break; //Temporalmente está así
                }
    }
    void ConstantChanges(){
        if(allPlatformLevels.Contains(SceneManager.GetActiveScene().name) && GameObject.Find("PlayerCar") && GameObject.Find("Capsule")){
            ChangePlayerToCar();
        }
    }
    
    void StartSettings(){
        CameraSettings(1,"Capsule",1);
    }
    void CameraSettings(int cameraMode, string followPlayer, int enablePanelUI){
        switch(cameraMode){
            case 1: camaraGlobal.GetComponent<PixelPerfectCamera>().enabled=false; camaraGlobal.GetComponent<CinemachineBrain>().enabled=true; break;
            case 2: camaraGlobal.GetComponent<PixelPerfectCamera>().enabled=true; camaraGlobal.GetComponent<CinemachineBrain>().enabled=false; break;
        }

        if(followPlayer!="")
        camaraGlobal._player=followPlayer;

        switch(enablePanelUI){
            case 1: camaraGlobal.attachedCanvas.platformUI.SetActive(true); camaraGlobal.attachedCanvas.platformUI.SetActive(false); camaraGlobal.attachedCanvas.platformUI.SetActive(false);
            break;
            case 2: camaraGlobal.attachedCanvas.platformUI.SetActive(false); camaraGlobal.attachedCanvas.platformUI.SetActive(true); camaraGlobal.attachedCanvas.platformUI.SetActive(false);
            break;
            case 3: camaraGlobal.attachedCanvas.platformUI.SetActive(false); camaraGlobal.attachedCanvas.platformUI.SetActive(false); camaraGlobal.attachedCanvas.platformUI.SetActive(true);
            break;
            default: break;
        }
    }

    void AudioSettings(string player){
        AudioManager.Instance.SetPlayer(player);
    }

    void SceneMusic(){
        Scene _scene=SceneManager.GetActiveScene();
        
        switch(_scene.name){
            case "MenuCar": MusicSettings("Plataformas Music",0,"Select Car Music",1f);  break;
            case "2": break;
            default: break;
            //case: "Otras que ya veremos"
        }
    }

    void MusicSettings(string prevMusic, float fadeOutTime, string newMusic, float fadeInTime){
        AudioManager.Instance.ChangeMusicTo(prevMusic,fadeOutTime,newMusic,fadeInTime);
    }
    void CarSettings(){

        if(menuScenes.Contains(currentScene.name))
            CarreraManager.Instance.killMouse=true;
        else if(allStages.Contains(currentScene.name)){
            MusicSettings("Select Car Music",0.25f,"",0);
            CarreraManager.Instance.killMouse=false; 
            CarreraManager.Instance?.SetRace();  
        }
        
    }

    void ChangePlayerToCar(){
            Destroy(GameObject.Find("Capsule"));
    }


    //Transiciones
    public void ApplyTransitionEffect(string nameFX, bool fluctuate, bool isTemporary, bool activate, float time){
        if(!isTemporary)
        camaraGlobal.cameraFX.ActivateEffect(nameFX,fluctuate,activate);
        else
        camaraGlobal.cameraFX.ActivateTemporaryEffect(nameFX,fluctuate,time);
    }

    public void ApplyTransitionEffect(TransitionData data){
        ApplyTransitionEffect(data.nameFX,data.fluctuate,data.isTemporary,data.activate,data.time);
    }
}
