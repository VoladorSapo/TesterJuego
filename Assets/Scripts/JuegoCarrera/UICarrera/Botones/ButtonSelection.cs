using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonSelection : MonoBehaviour, ISelectHandler
{   
    [Header("Display Frames")]
    [SerializeField] protected UnityEngine.UI.Image imageDisplay;
    [SerializeField] protected TextMeshProUGUI textDisplay;
    
    [Header("Button Properties")]
    [SerializeField] public Sprite thisButtonImage;
    [SerializeField] public string thisDescription;


    public virtual void OnSelect(BaseEventData eventData)
    {
        imageDisplay.sprite=thisButtonImage;
        textDisplay.text=thisDescription;
        AudioManager.Instance.PlaySound("Menu confirm pixel",false,transform.position,false);
    }


}
