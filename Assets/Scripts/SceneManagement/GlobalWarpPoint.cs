using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalWarpPoint : MonoBehaviour
{  
    [Header("Warpoint Settings")]
    public bool oneUse; private bool used=false;

    [Header("Scene Change (Vacio == No Cambia Escena)")]
    public string nextScene;

    [Header("Lista de Efectos de Transición")]
    public List<TransitionData> transitionList=new List<TransitionData>();
    public float WaitToChange;


    [Header("\nPosition Change")]
    public Vector3 nextPosition;

    [Header("Narrative Change (-1 == No Cambia)")]
    public int nextNarrativePart;

    [Header("Music Change")]
    public string currentMusic;
    public string followingMusic;
    public float fadeOutTime, fadeInTime;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player" && !used){
            if(oneUse){used=true;}
            AudioManager.Instance?.ChangeMusicTo(currentMusic,fadeOutTime,followingMusic,fadeInTime);

            if(nextNarrativePart>=0){SceneManagement.Instance.narrativePart=nextNarrativePart;}
            
            //SceneManagement.Instance.instantiatePos=nextPosition;
            if(other.GetComponent<Rigidbody2D>()!=null){
                //other.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            }

            if(nextScene!=""){
                if(transitionList.Count>0)
                IterateTransitions();

                DontDestroyOnLoad(other.gameObject);

                if(WaitToChange<=0){
                other.gameObject.transform.position=nextPosition;
                SceneManager.LoadSceneAsync(nextScene);
                }else
                StartCoroutine(HoldTransition(nextScene,WaitToChange,other));
            }
            //other.gameObject.transform.position=nextPosition;
            
            
            
        }
    }

    IEnumerator HoldTransition(string nextScene, float WaitToChange, Collider2D other){
        yield return new WaitForSeconds(WaitToChange);
        other.gameObject.transform.position=nextPosition;
        SceneManager.LoadSceneAsync(nextScene);
    }
    void IterateTransitions(){
        foreach(TransitionData td in transitionList){
            SceneManagement.Instance.ApplyTransitionEffect(td.nameFX,td.fluctuate,td.isTemporary,td.activate, td.time);
        }
    }

}
