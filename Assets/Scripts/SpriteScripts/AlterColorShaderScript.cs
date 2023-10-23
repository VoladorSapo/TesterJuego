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
        Material mat1=GetComponent<SpriteRenderer>().material;
        mat1.SetInt("_ColorCount",ogColors.Length);
        mat1.SetColorArray("_Colors",ogColors);
        mat1.SetColorArray("_NewColors",newColors);
    }
}
