using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextoMenuZelda : MonoBehaviour
{
    [SerializeField] private float waitTime;
    private float waitTimer;

    void Start(){
        waitTimer=waitTime;
    }
    void Update()
    {
        if(waitTimer>0){
            waitTimer-=Time.deltaTime;
        }

        if(Input.anyKeyDown && waitTimer<=0){
            SceneManager.LoadScene("Intro");
        }
    }
}
