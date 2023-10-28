using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterColorShaderScript : MonoBehaviour
{
    public Material cloneMaterial;
    public Color[] ogColors;
    public Color[] newColors;
    [SerializeField] bool isGlitched=false;
    Material mat;
    void Awake(){
        mat=Instantiate<Material>(cloneMaterial);
        GetComponent<SpriteRenderer>().material=mat;

        if(isGlitched)
        InvokeRepeating("GlitchPencil",0f,1.15f);
    }

    void Update(){
        
        GetComponent<SpriteRenderer>().material.SetInt("_ColorCount",ogColors.Length);
        GetComponent<SpriteRenderer>().material.SetColorArray("_Colors",ogColors);
        GetComponent<SpriteRenderer>().material.SetColorArray("_NewColors",newColors);
    }

    public void GlitchPencil(){
        StartCoroutine(IEnumGlitchPencilStage2());
    }

    public IEnumerator IEnumGlitchPencilStage2(){
        
        Material mat = GetComponent<SpriteRenderer>().material;
        
        mat.SetFloat("_moveX",0.5f);
        yield return new WaitForSeconds(0.25f);
        mat.SetFloat("_moveY",0.5f);
        yield return new WaitForSeconds(0.5f);
        mat.SetFloat("_moveX",0f); mat.SetFloat("_moveY",0f);
        yield return new WaitForSeconds(0.25f);
        mat.SetFloat("_moveX",0.5f);
        yield return new WaitForSeconds(0.25f);
        mat.SetFloat("_moveX",0f); mat.SetFloat("_moveY",0f);

    }
}
