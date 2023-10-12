using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalWarpPoint : MonoBehaviour
{
    [Header("Scene Change (Vacio == No Cambia Escena)")]
    public string nextScene;

    [Header("Position Change")]
    public Vector3 nextPosition;

    [Header("Narrative Change (-1 == No Cambia)")]
    public int nextNarrativePart;

    [Header("Music Change")]
    public string currentMusic;
    public string followingMusic;
    public float fadeOutTime, fadeInTime;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            AudioManager.Instance?.ChangeMusicTo(currentMusic,fadeOutTime,followingMusic,fadeInTime);

            if(nextNarrativePart>=0){SceneManagement.Instance.narrativePart=nextNarrativePart;}
            
            //SceneManagement.Instance.instantiatePos=nextPosition;
            other.gameObject.transform.position=nextPosition;
            if(other.GetComponent<Rigidbody2D>()!=null){
                other.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            }

            if(nextScene!=""){
                DontDestroyOnLoad(other.gameObject);
                SceneManager.LoadSceneAsync(nextScene);
            }
            //other.gameObject.transform.position=nextPosition;
            
            
            
        }
    }

}
