using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetSelect : MonoBehaviour
{
    [SerializeField] Button button;
    [Header("Display Frames")]
    [SerializeField] protected UnityEngine.UI.Image imageDisplay;
    [SerializeField] protected TextMeshProUGUI textDisplay;
    void OnEnable(){
        EventSystem.current.SetSelectedGameObject(button.gameObject, new BaseEventData(EventSystem.current));
        imageDisplay.sprite=button.GetComponent<StageButton>().thisButtonImage;
        textDisplay.text=button.GetComponent<StageButton>().thisDescription;
    }
}
