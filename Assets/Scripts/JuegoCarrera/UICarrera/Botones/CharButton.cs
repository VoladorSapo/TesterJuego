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
        CarreraManager.Instance.SetPlayerSprite(SpriteIndex);
        CarreraManager.Instance.PlayRace();
    }
}
