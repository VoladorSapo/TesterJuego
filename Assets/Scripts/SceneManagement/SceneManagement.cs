using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    public int narrativePart;

    //Escenas plataforma:
    //...

    [Header("Escenas del Juego de Carreras")]
    public List<string> allStages;
    public List<string> menuScenes;

    //Escenas Zelda:
    //...

    //Escenas de control
    Scene currentScene;
    Scene previousScene;

    void Awake(){
        narrativePart=4;
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
            
            switch(narrativePart){
                case 0: break;
                case 1: break;
                case 2: break;
                default: CarSettings(); break; //Temporalmente está así
            }

        }
    }

    void CarSettings(){

        if(menuScenes.Contains(currentScene.name))
            CarreraManager.Instance.killMouse=true;
        else if(allStages.Contains(currentScene.name)){
            CarreraManager.Instance.killMouse=false; 
            CarreraManager.Instance?.SetRace();  
        }
        
    }
}
