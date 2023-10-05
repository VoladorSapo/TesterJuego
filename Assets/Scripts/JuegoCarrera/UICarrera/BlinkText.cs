using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
public class BlinkText : MonoBehaviour
{
    [SerializeField] private float blinkTime;
    [SerializeField] private GameObject nextMenu;
    public AnimationCurve blinkCurve;
    private float blinkTimer;
    bool isVisible=true; bool pressedEnter=false;
    TextMeshProUGUI text;
    void Start(){
        blinkTimer=blinkTime;
        text=GetComponent<TextMeshProUGUI>();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            EnterPressed();
        }

        if(blinkTimer>0){
            blinkTimer-=Time.deltaTime;
        }
        if(blinkTimer<=0 && !pressedEnter){
            isVisible=!isVisible;

            if(isVisible){
                text.color=new Color(text.color.r,text.color.g,text.color.b,0);
            }else{
                text.color=new Color(text.color.r,text.color.g,text.color.b,255);
            }

            blinkTimer=blinkTime;
        }
    }

    void EnterPressed(){
        text.color=new Color(text.color.r,text.color.g,text.color.b,255);
        pressedEnter=true;
        StartCoroutine(startBlinkPanel());
    }
    IEnumerator startBlinkPanel(){
        float curveTime=0f;
        float visibility;
        while(curveTime<=3f){
            curveTime+=Time.deltaTime*4.5f;
            visibility=blinkCurve.Evaluate(curveTime);
            if(Mathf.RoundToInt(visibility)>=1){
                transform.parent.GetComponent<CanvasGroup>().alpha=1;
            }else{
                transform.parent.GetComponent<CanvasGroup>().alpha=0;
            }
            yield return null;
        }
        transform.parent.gameObject.SetActive(false);
        nextMenu.SetActive(true);
        
    }
}
