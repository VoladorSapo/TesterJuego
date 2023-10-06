using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageButton : ButtonSelection
{
    [SerializeField] private int stageIndex;
    [SerializeField] private int lapsOfStage;
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public void SetLaps(){
        CarreraManager.Instance.indexSelectedStage=stageIndex;
        CarreraManager.Instance.numberOfLaps=lapsOfStage;
    }
}
