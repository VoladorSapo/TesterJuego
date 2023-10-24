using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    public int globalNarrativePart;
    private int globalPrevNarrativePart=-1;
    public string actionName;

    public int localNarrativePart=-1;

    [Header("Escenas del Juego de Plataformas")]
    public List<string> allPlatformLevels;
    //...

    [Header("Escenas del Juego de Carreras")]
    public List<string> allStages;
    public List<string> menuScenes;

    [Header("Escenas del Juego Zelda")]

    public List<string> allZeldaScenes;
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

    void StartSettings(){
         //CameraSettings(1,"Capsule",1);
         CameraSettings(2,"PlayerCar",2);
    }

    public void SetNextScene(string action){
        actionName=action;
    }
    void Update()
    {   
        currentScene=SceneManager.GetActiveScene();

        if(Input.GetKeyDown(KeyCode.L)){
            GameObject.FindObjectOfType<GlobalWarpPoint>().DoTransition();
        }

        if(previousScene==null || previousScene.name!=currentScene.name){
            previousScene=currentScene;
            GameChanges();
            BeginSceneWith(ref actionName);
        }

        //Cambios Narrativos que no dependen del cambio de escenas
        if(localNarrativePart>=0){
                NarrativeChanges();
        }

        ConstantChanges();
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    StartSettings();
        //    //CameraSettings(1, "Capsule", 1);

        //}
    }

    void SpawnZeldaPlayer(){
        
        int numberOfPlayers=GameObject.FindGameObjectsWithTag("Player").Where(obj=>obj.GetComponent<PlayerMoveScript>()!=null).Count();
        if(numberOfPlayers==0){
            GameObject go=Instantiate(zeldaPrefab,posZeldaInicio,Quaternion.identity);
            go.name=zeldaPrefab.name;
        }
    }

    void GameChanges(){

        SceneMusic();
        //Cambios Narrativos que dependen del cambio de escenas
        if(globalPrevNarrativePart!=globalNarrativePart){
                globalPrevNarrativePart=globalNarrativePart;
                switch(globalNarrativePart){
                    case 0: break;
                    case 1: CameraSettings(1, "Capsule", 1); AudioSettings("Capsule"); break;
                    case 2: CameraSettings(2,"PlayerCar",-1); AudioSettings("PlayerCar"); break;
                    case 3: break;
                    case 4: SpawnZeldaPlayer(); CameraSettings(2,"ZeldaPlayer",-1); AudioSettings("ZeldaPlayer"); break;
                    default: break; //Temporalmente está así
                }
        }
        
    }

    void BeginSceneWith(ref string act){
        Debug.LogWarning(act);
        switch(act){
            case "killMouse": killMouse=true; break;
            case "reviveMouse": killMouse=false; break;
            case "SetRaceNormal": CarSettings(false,false); break;
            case "SetRaceGlitch": CarSettings(false,true); break;
            case "SetRaceStage2": CarSettings(false,true); EventManager.Instance?.GlitchPencilStage2(); break;
            case "StopRace": CarSettings(true,true); break;
        }
        act="";
    }



    void NarrativeChanges(){
       
       Debug.LogWarning(localNarrativePart);
        if(allPlatformLevels.Contains(SceneManager.GetActiveScene().name)){
            switch(localNarrativePart){
                    case 0: break;
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    default: break;
                }
        }else if(allStages.Contains(SceneManager.GetActiveScene().name)){
            
            switch(localNarrativePart){
                    case 0: CarSettings(false, false); break;
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    default: break;
                }
        }else if(allZeldaScenes.Contains(SceneManager.GetActiveScene().name)){
            switch(localNarrativePart){
                    case 0: break;
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    default: break;
                }
        }
        localNarrativePart=-1;
    }

    bool killMouse=false;
    void ConstantChanges(){
        if(allPlatformLevels.Contains(SceneManager.GetActiveScene().name) && GameObject.Find("PlayerCar") && GameObject.Find("Capsule")){
            ChangePlayerToCar();
        }

        if(killMouse){
            KillMouseInputs();
        }
    }
    
    void CameraSettings(int cameraMode, string followPlayer, int enablePanelUI){
        switch(cameraMode){
            case 1: camaraGlobal.GetComponent<PixelPerfectCamera>().enabled=false; camaraGlobal.GetComponent<CinemachineBrain>().enabled=true; break;
            case 2: camaraGlobal.GetComponent<PixelPerfectCamera>().enabled=true; camaraGlobal.GetComponent<CinemachineBrain>().enabled=false; break;
        }

        if(followPlayer!="")
        camaraGlobal._player=followPlayer;


       Debug.Log(enablePanelUI);
        switch(enablePanelUI){
            case 1: camaraGlobal.attachedCanvas.platformUI.SetActive(true); camaraGlobal.attachedCanvas.carUI.SetActive(false); camaraGlobal.attachedCanvas.zeldaUI.SetActive(false);
            break;
            case 2: camaraGlobal.attachedCanvas.platformUI.SetActive(false); camaraGlobal.attachedCanvas.carUI.SetActive(true); camaraGlobal.attachedCanvas.zeldaUI.SetActive(false);
            break;
            case 3: camaraGlobal.attachedCanvas.platformUI.SetActive(false); camaraGlobal.attachedCanvas.carUI.SetActive(false); camaraGlobal.attachedCanvas.zeldaUI.SetActive(true);
            break;
            default: break;
        }
    }

    void AudioSettings(string player){
        AudioManager.Instance?.SetPlayer(player);
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
        AudioManager.Instance?.ChangeMusicTo(prevMusic,fadeOutTime,newMusic,fadeInTime);
    }
    void CarSettings(bool stopRace,bool glitchedMapActive){
        
        if(stopRace){
            CarreraManager.Instance.StopRace();
        }
        else{
            if(menuScenes.Contains(currentScene.name))
                CarreraManager.Instance.killMouse=true;
            else if(allStages.Contains(currentScene.name)){
                MusicSettings("Select Car Music",0.25f,"",0);
                CarreraManager.Instance.killMouse=false; 
                CarreraManager.Instance?.SetRace(glitchedMapActive);  
            }
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


    GameObject lastSelected;
    public void KillMouseInputs(){
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
        if(EventSystem.current?.currentSelectedGameObject==null){
            if(lastSelected!=null)
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }else if(EventSystem.current.currentSelectedGameObject!=null){
            lastSelected=EventSystem.current.currentSelectedGameObject;
        }
    }
}
