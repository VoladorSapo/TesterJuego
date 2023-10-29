using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : ButtonSelection
{
    [SerializeField] private int SpriteIndex;
    protected override void Start()
    {
        SetPauseEvents();
    }
    public override void OnSelect(BaseEventData eventData)
    {
        
        base.OnSelect(eventData);
    }

    public void SetPlayer(){
        if(!GetComponent<Button>().enabled){return;}
        AudioManager.Instance.PlaySound("Menu confirm platform",false,transform.position,false);
        CarreraManager.Instance.SetPlayerSprite(SpriteIndex);
        
        if( SceneManagement.Instance.actionName=="")
        SceneManagement.Instance.actionName="SetRaceNormal";
        SceneManagement.Instance.globalChange=2;
        
        CarreraManager.Instance.GoToRace();
    }

    Navigation nav;
    Color newCol;
    public override void Pause(){
        if(this==null){return;}
        if(EventSystem.current?.currentSelectedGameObject!=null && EventSystem.current?.currentSelectedGameObject.name==this.gameObject.name){
           
            ColorUtility.TryParseHtmlString("#FF0000", out newCol);
            GetComponent<Image>().color=newCol;
        }
        Button button=GetComponent<Button>();
        button.enabled=false;
    }

    public override void Unpause(){
        if(this==null){return;}
        if(EventSystem.current?.currentSelectedGameObject!=null &&  EventSystem.current?.currentSelectedGameObject.name==this.gameObject.name){
            ColorUtility.TryParseHtmlString("#FFFFFF", out newCol);
            GetComponent<Image>().color=newCol;
        }
        Button button=GetComponent<Button>();
        button.enabled=true;
    }

    public override void SetPauseEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause,Unpause);
    }
}
