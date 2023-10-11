using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    public int narrativePart;
    [SerializeField] private int prevNarrativePart;

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


    void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        } //Desde la primera escena habra un SceneManager
    }
    void Start(){
        currentScene=SceneManager.GetActiveScene();
    }
    void Update()
    {   
        currentScene=SceneManager.GetActiveScene();

        if(previousScene==null || previousScene.name!=currentScene.name){
            previousScene=currentScene;
            //SpawnPlayerAt();
            Debug.Log("jaid");
            if(prevNarrativePart!=narrativePart){
                prevNarrativePart=narrativePart;
                switch(narrativePart){
                    case 0: break;
                    case 1: break;
                    case 2: CarSettings(); break;
                    case 3: ChangePlayerToCar(); break;
                    case 4: SpawnZeldaPlayer(); break;
                    default: CarSettings(); break; //Temporalmente está así
                }
            }

        }

        ConstantChanges();
    }

    void SpawnZeldaPlayer(){
        
        int numberOfPlayers=GameObject.FindGameObjectsWithTag("Player").Where(obj=>obj.GetComponent<PlayerMoveScript>()!=null).Count();
        if(numberOfPlayers==0){
            Instantiate(zeldaPrefab,posZeldaInicio,Quaternion.identity);
        }
    }
    void ConstantChanges(){
        if(allPlatformLevels.Contains(SceneManager.GetActiveScene().name) && GameObject.Find("PlayerCar")){
            Debug.Log("w");
            ChangePlayerToCar();
        }
    }
    

    void CarSettings(){

        if(menuScenes.Contains(currentScene.name))
            CarreraManager.Instance.killMouse=true;
        else if(allStages.Contains(currentScene.name)){
            Debug.Log("carrera");
            CarreraManager.Instance.killMouse=false; 
            CarreraManager.Instance?.SetRace();  
        }
        
    }

    void ChangePlayerToCar(){
            Destroy(GameObject.Find("Capsule"));
    }
}
