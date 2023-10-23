using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("Event Pencil Stage 2")]
    [SerializeField] GameObject pencilBug;
    [SerializeField] Material defaultMat;
    void Awake(){
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }

    public void GlitchPencilStage2(){
        StartCoroutine(IEnumGlitchPencilStage2());
    }
    public IEnumerator IEnumGlitchPencilStage2(){
        GameObject go=Instantiate(pencilBug,new Vector3(-9.5f,-2.5f,0),Quaternion.Euler(0, 0, -90));
        
        go.GetComponent<PolygonCollider2D>().enabled=false;
        go.GetComponent<SpriteRenderer>().sortingLayerName="CarLayer";
        go.GetComponent<SpriteRenderer>().sortingOrder=1;
        Material mat = go.GetComponent<SpriteRenderer>().material;
        AlterColorShaderScript shaderScript=go.GetComponent<AlterColorShaderScript>();
        
        mat.SetFloat("_moveX",0.5f);
        yield return new WaitForSeconds(0.75f);
        mat.SetFloat("_moveY",0.5f);
        yield return new WaitForSeconds(1f);
        mat.SetFloat("_moveX",0f); mat.SetFloat("_moveY",0f);
        yield return new WaitForSeconds(0.25f);
        mat.SetFloat("_moveX",0.5f);
        yield return new WaitForSeconds(0.25f);
        mat.SetFloat("_moveX",0f); mat.SetFloat("_moveY",0f);
        /*yield return new WaitForSeconds(0.25f);
        go.GetComponent<SpriteRenderer>().sharedMaterial=defaultMat;
        yield return new WaitForSeconds(0.05f);
        go.GetComponent<SpriteRenderer>().sharedMaterial=mat;*/

        
    }
}
