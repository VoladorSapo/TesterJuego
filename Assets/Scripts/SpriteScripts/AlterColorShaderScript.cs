using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterColorShaderScript : MonoBehaviour
{
    public Color[] ogColors;
    public Color[] newColors;

    Material mat;
    void Awake(){
        mat=GetComponent<SpriteRenderer>().material;
    }

    void Update(){
        mat.SetInt("_ColorCount",ogColors.Length);
        mat.SetColorArray("_Colors",ogColors);
        mat.SetColorArray("_NewColors",newColors);
    }
}
