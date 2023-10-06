using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideUI : MonoBehaviour
{
    [Header("Animation Properties")]
    [SerializeField] private float distanceLeft;
    [SerializeField] private float time;
    [SerializeField] private GameObject otherPanel;

    [Header("Display Frames")]
    [SerializeField] UnityEngine.UI.Image imageDisplay;
    [SerializeField] TextMeshProUGUI textDisplay;

    [Header("SetButton")]
    [SerializeField] private Button button;
    public void SlideLeft(){
        textDisplay.text="";
        imageDisplay.sprite=null;
        EventSystem.current.SetSelectedGameObject(imageDisplay.gameObject);
        transform.LeanMoveLocal(new Vector3(transform.localPosition.x-distanceLeft,transform.localPosition.y,0),time).setEaseOutQuad();
        otherPanel.transform.LeanMoveLocal(new Vector3(otherPanel.transform.localPosition.x-distanceLeft,otherPanel.transform.localPosition.y,0),time).setEaseOutQuad().setOnComplete(SetButton);
    }

    void SetButton(){
        EventSystem.current.SetSelectedGameObject(button.gameObject, new BaseEventData(EventSystem.current));
    }
}
