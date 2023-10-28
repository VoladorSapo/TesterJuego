using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PixelText : MonoBehaviour
{
    [SerializeField] Material textMaterialClone;
    [Range(0f,1f)]
    [SerializeField] float outlineThickness;
    Material mat;

    void Awake(){
        mat=Instantiate<Material>(textMaterialClone);
        GetComponent<TextMeshProUGUI>().fontMaterial=mat;
        mat.SetFloat("_OutlineWidth",outlineThickness);
    }
}
