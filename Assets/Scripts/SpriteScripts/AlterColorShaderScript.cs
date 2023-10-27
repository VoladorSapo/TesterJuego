using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterColorShaderScript : MonoBehaviour
{
    public Material cloneMaterial;
    public Color[] ogColors;
    public Color[] newColors;

    Material mat;
    void Awake(){
        mat=Instantiate<Material>(cloneMaterial);
        GetComponent<SpriteRenderer>().material=mat;
    }

    void Update(){
        
        GetComponent<SpriteRenderer>().material.SetInt("_ColorCount",ogColors.Length);
        GetComponent<SpriteRenderer>().material.SetColorArray("_Colors",ogColors);
        GetComponent<SpriteRenderer>().material.SetColorArray("_NewColors",newColors);
    }
}
