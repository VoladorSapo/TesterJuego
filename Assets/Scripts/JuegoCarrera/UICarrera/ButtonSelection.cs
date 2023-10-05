using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonSelection : MonoBehaviour
{   
    [Header("Display Frames")]
    [SerializeField] UnityEngine.UI.Image imageDisplay;
    [SerializeField] TextMeshProUGUI textDisplay;
    
    [Header("Button Properties")]
    [SerializeField] Sprite thisButtonImage;
    [SerializeField] string thisDescription;
    void Update(){
        if(EventSystem.current.currentSelectedGameObject==this.gameObject){
            imageDisplay.sprite=thisButtonImage;
            textDisplay.text=thisDescription;
        }
    }
}
