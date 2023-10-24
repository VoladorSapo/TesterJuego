using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharButton : ButtonSelection
{
    [SerializeField] private int SpriteIndex;
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public void SetPlayer(){
        AudioManager.Instance.PlaySound("Menu confirm platform",false,transform.position,false);
        CarreraManager.Instance.SetPlayerSprite(SpriteIndex);
        
        SceneManagement.Instance.actionName="SetRaceNormal";
        CarreraManager.Instance.GoToRace();
    }
}
