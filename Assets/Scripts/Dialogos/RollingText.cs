using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollingText : MonoBehaviour
{
    public TMP_Text textComponent;

    [Header("Scroll Configuration")]
    public float time;
    public float timeForNext;
    [SerializeField] float distanceUp;
    [SerializeField] bool canGlitch=true;
    [SerializeField] float glitchTime;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    
    float glitchTimer=-1;
    bool passedMid=false;

    [Header("Name")]
    [SerializeField] string realName;
    void Start()
    {
        
    }

    public void ScrollUp(){
        transform.LeanMoveLocal(new Vector3(transform.localPosition.x,transform.localPosition.y+distanceUp,0),time);
    }
    // Update is called once per frame
    void Update()
    {   
        if(!canGlitch){return;}

        textComponent.ForceMeshUpdate();

        if(IsInMiddle() && !passedMid){
            passedMid=true;
            
           glitchTimer=glitchTime;
            textComponent.fontMaterial.SetFloat("_Sharpness",-1);
        }
        
        if(glitchTimer>=0){
            GlitchText();
            glitchTimer-=Time.deltaTime;
            if(glitchTimer<0){
                textComponent.text=realName;
                textComponent.fontMaterial.SetFloat("_Sharpness",0);
            }
        }

        
        
    }

    void GlitchText(){
        Debug.LogWarning("dsa");
        var textInfo=textComponent.textInfo;
        var mesh = textComponent.mesh;
        for(int i=0; i<textInfo.characterCount; ++i){
            var charInfo = textInfo.characterInfo[i];

            if(!charInfo.isVisible){
                continue;
            }
            var verts=textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            for(int j=0; j<4; ++j){
                var orig=verts[charInfo.vertexIndex+j];
                verts[charInfo.vertexIndex+j]=
                orig+new Vector3(Mathf.Sin(Time.time*6f*Random.Range(-10f,10f))*10f,Mathf.Sin(Time.time*6f*Random.Range(-10f,10f))*10f,0);

                
            }
                       

            
        }

        Color[] colors= textComponent.mesh.colors;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
                TMP_CharacterInfo c = textComponent.textInfo.characterInfo[i];
 
                int index = c.vertexIndex;

                
                colors[index] = new Color(Random.Range(startColor.r, endColor.r),Random.Range(startColor.g, endColor.g),Random.Range(startColor.b, endColor.b));
                colors[index + 1] = new Color(Random.Range(startColor.r, endColor.r),Random.Range(startColor.g, endColor.g),Random.Range(startColor.b, endColor.b));
                colors[index + 2] = new Color(Random.Range(startColor.r, endColor.r),Random.Range(startColor.g, endColor.g),Random.Range(startColor.b, endColor.b));
                colors[index + 3] = new Color(Random.Range(startColor.r, endColor.r),Random.Range(startColor.g, endColor.g),Random.Range(startColor.b, endColor.b));          
        }
        mesh.colors = colors;
        
        for(int i=0; i<textInfo.meshInfo.Length; ++i){
            var meshInfo=textInfo.meshInfo[i];
            meshInfo.mesh.vertices=meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh,i);
        }

        textComponent.canvasRenderer.SetMesh(mesh);
    }
    bool IsInMiddle(){

        Vector2 screenPosition = Camera.main.WorldToViewportPoint(this.transform.position);
        Vector2 middleOfScreen = Camera.main.WorldToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
       
        return Vector2.Distance(screenPosition, middleOfScreen) <= 30f;


    }
}
