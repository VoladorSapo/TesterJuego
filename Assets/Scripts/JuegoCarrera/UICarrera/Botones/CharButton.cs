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
        
        CarreraManager.Instance.GoToRace();
    }

    Navigation nav;
    public override void Pause(){
        Button button=GetComponent<Button>();
        button.enabled=false;
    }

    public override void Unpause(){
        Button button=GetComponent<Button>();
        button.enabled=true;
    }

    public override void SetPauseEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause,Unpause);
    }
}
