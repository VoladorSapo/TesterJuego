using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetSelect : MonoBehaviour
{
    [SerializeField] Button button;
    void OnEnable(){
        EventSystem.current.SetSelectedGameObject(button.gameObject, new BaseEventData(EventSystem.current));
    }
}
