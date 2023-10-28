using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalWarpPoint : MonoBehaviour
{  
    [Header("Warpoint Settings")]
    public bool transitionWithPlayer=true;
    public bool oneUse; private bool used=false;
    public bool forceTransformChange;

    [Header("Scene Change (Vacio == No Cambia Escena)")]
    public string nextScene;
    public string nextAcctionName;

    [Header("Lista de Efectos de Transición")]
    public List<TransitionData> transitionList=new List<TransitionData>();
    public float WaitToChange;


    [Header("\nPosition Change")]
    public Vector3 nextPosition;
    public Vector3 nextRotation;

    [Header("Narrative Change (-1 == No Cambia)")]
    [Header("Depende del cambio de Escena")]
    public int nextGlobalNarrativePart;

    [Header("No Depende del cambio de Escena")]
    public NarrativeParts nextLocalNarrativeParts;

    [Header("Music Change")]
    public string currentMusic;
    public string followingMusic;
    public float fadeOutTime, fadeInTime;

    void OnTriggerEnter2D(Collider2D other){

        if(other.tag!="Player"){return;}

        SceneManager.MoveGameObjectToScene(other.gameObject,SceneManager.GetActiveScene());

        if(forceTransformChange){
            SetPlayerTransform(other.gameObject);
        }

        if(transitionWithPlayer)
        DoTransitionWithPlayer(other);
        else
        DoTransition();
    }

    IEnumerator HoldTransition(string nextScene, float WaitToChange, Collider2D other){
        yield return new WaitForSeconds(WaitToChange);
        if(other!=null){
        other.gameObject.transform.position=nextPosition;

        }
        
        SceneManager.LoadScene(nextScene);
    }
    void IterateTransitions(){
            CamaraGlobal.Instance.cameraFX.ApplyEffects(transitionList);
    }

    public void DoTransitionWithPlayer(Collider2D other){
        if(other.tag=="Player" && !used){
            
            if(oneUse){used=true;}

            AudioManager.Instance?.ChangeMusicTo(currentMusic,fadeOutTime,followingMusic,fadeInTime);

            if(nextGlobalNarrativePart>=0){SceneManagement.Instance.globalChange=nextGlobalNarrativePart;}
            ApplyNarrativeChanges();
            
            //SceneManagement.Instance.instantiatePos=nextPosition;
            if(other.GetComponent<Rigidbody2D>()!=null){
                //other.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            }

            if(transitionList.Count>0)
                IterateTransitions();
                
            if(nextScene!=""){
                SceneManagement.Instance.actionName=nextAcctionName;
                DontDestroyOnLoad(other.gameObject);

                if(WaitToChange<=0){
                    SetPlayerTransform(other.gameObject);
                    SceneManager.LoadScene(nextScene);
                }else
                StartCoroutine(HoldTransition(nextScene,WaitToChange,other));
            }
            //other.gameObject.transform.position=nextPosition;
        }
    }

    public void DoTransitionWithPlayer(GameObject other){
        if(other.tag=="Player" && !used){
            if(oneUse){used=true;}
            AudioManager.Instance?.ChangeMusicTo(currentMusic,fadeOutTime,followingMusic,fadeInTime);

            if(nextGlobalNarrativePart>=0){SceneManagement.Instance.globalChange=nextGlobalNarrativePart;}
            ApplyNarrativeChanges();
            
            //SceneManagement.Instance.instantiatePos=nextPosition;
            if(other.GetComponent<Rigidbody2D>()!=null){
                //other.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            }

            if(transitionList.Count>0)
                IterateTransitions();
            
            if(nextScene!=""){
                SceneManagement.Instance.actionName=nextAcctionName;
                

                DontDestroyOnLoad(other.gameObject);

                if(WaitToChange<=0){
                SetPlayerTransform(other);
                SceneManager.LoadScene(nextScene);
                }else
                StartCoroutine(HoldTransition(nextScene,WaitToChange,other.GetComponent<Collider2D>()));
            }
            //other.gameObject.transform.position=nextPosition;
        }
    }

    public void DoTransition(){
        if(!used){
            if(oneUse){used=true;}
            AudioManager.Instance?.ChangeMusicTo(currentMusic,fadeOutTime,followingMusic,fadeInTime);

            if(nextGlobalNarrativePart>=0){SceneManagement.Instance.globalChange=nextGlobalNarrativePart;}
            ApplyNarrativeChanges();
            
            if(transitionList.Count>0)
                IterateTransitions();
            if(nextScene!=""){
                SceneManagement.Instance.actionName=nextAcctionName;
                


                if(WaitToChange<=0){
                SceneManager.LoadScene(nextScene);
                }else
                StartCoroutine(HoldTransition(nextScene,WaitToChange,null));
            }
            
            //other.gameObject.transform.position=nextPosition;
        }
    }

    void ApplyNarrativeChanges(){
        if(nextLocalNarrativeParts.PlatformNarrative>=0){SceneManagement.Instance.narrativeParts.PlatformNarrative=nextLocalNarrativeParts.PlatformNarrative;}
        if(nextLocalNarrativeParts.CarNarrative>=0){SceneManagement.Instance.narrativeParts.CarNarrative=nextLocalNarrativeParts.CarNarrative;}
        if(nextLocalNarrativeParts.ZeldaNarrative>=0){SceneManagement.Instance.narrativeParts.ZeldaNarrative=nextLocalNarrativeParts.ZeldaNarrative;}
    }

    void SetPlayerTransform(GameObject other){
        other.gameObject.transform.position=nextPosition;
            if(other.gameObject.GetComponent<CarController>()!=null){
                other.gameObject.GetComponent<CarController>()._realRotationAngle=nextRotation.z;
            }else{
                other.gameObject.GetComponent<Rigidbody2D>().SetRotation(Quaternion.Euler(nextRotation));
            }
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(nextRotation), Vector3.one);
            other.gameObject.GetComponent<Rigidbody2D>().velocity=rotationMatrix*other.gameObject.GetComponent<Rigidbody2D>().velocity;
    }

}
