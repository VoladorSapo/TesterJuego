using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;

    [Header("Escenas del Juego de Carreras")]
    public List<string> allStages;
    public List<string> menuScenes;
    Scene currentScene;
    Scene previousScene;

    void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
    void Start(){
        currentScene=SceneManager.GetActiveScene();
    }
    void Update()
    {   
        currentScene=SceneManager.GetActiveScene();

        if(previousScene==null || previousScene.name!=currentScene.name){
            previousScene=currentScene;
            CarSettings();

        }
    }

    void CarSettings(){

        if(!allStages.Contains(currentScene.name)){
            if(menuScenes.Contains(currentScene.name))
            CarreraManager.Instance.KillMouseInputs();
        }
        else{
            CarreraManager.Instance.SetRace();
        }
    }
}
