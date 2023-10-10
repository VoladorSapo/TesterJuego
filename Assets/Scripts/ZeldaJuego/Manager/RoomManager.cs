using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    static public RoomManager Instance;
    public string currentScene;
    private string previousScene;

    [Header("Player")]
    [HideInInspector] private Vector3 playerPos;
    public Transform player;


    void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }

        currentScene=SceneManager.GetActiveScene().name;
        previousScene=currentScene;
    }

    public void ChangeToRoomAt(string newSceneName, Vector3 atPos){
        playerPos=atPos;
        SceneManager.LoadScene(newSceneName);
    }

    public void ChangeMusicTo(string currMusic, string newMusic, float fadeOutTime, float fadeInTime){
        AudioManager.Instance?.ChangeMusicTo(currMusic,fadeOutTime,newMusic,fadeInTime);
    }

    void Update(){
        currentScene=SceneManager.GetActiveScene().name;
        if(previousScene!=currentScene){
            player=GameObject.FindGameObjectWithTag("ZeldaPlayer").transform;
            Camera cam=Camera.main;
            cam.transform.position=new Vector3(playerPos.x, playerPos.y, cam.transform.position.z);

            previousScene=currentScene;
            player.transform.position=playerPos;
        }
    }
}
