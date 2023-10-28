using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlinkText : MonoBehaviour, IPauseSystem
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
        SetPauseEvents();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return) && !pressedEnter){
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
                if(visibility>=0.99f)
                AudioManager.Instance.PlaySound("Menu confirm pixel",false,transform.position,false);
            }else{
                transform.parent.GetComponent<CanvasGroup>().alpha=0;
            }
            yield return null;
        }
        transform.parent.gameObject.SetActive(false);
        nextMenu.SetActive(true);
        
    }

    public void Pause()
    {
        text.color=new Color(text.color.r,text.color.g,text.color.b,255);
        this.enabled=false;
    }

    public void Unpause()
    {
        this.enabled=true;
    }

    public void SetPauseEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause,Unpause);
    }
}
