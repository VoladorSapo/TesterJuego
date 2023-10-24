using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageButton : ButtonSelection
{
    [SerializeField] private string stageName;
    [SerializeField] private int lapsOfStage;
    [SerializeField] private int oneLapWP;
    [SerializeField] private int buttonID;

    protected override void Update(){
        if(Input.GetKey(KeyCode.G)){
            GamesManager.Instance.CarButtonStage(buttonID,this.GetComponent<Button>());
        }
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    protected override void Start()
    {
        Button button = GetComponent<Button>();
        if(GamesManager.Instance.unlockedCarStages<buttonID){GetComponent<Image>().color = Color.gray;}
        GamesManager.Instance.CarButtonStage(buttonID,this.GetComponent<Button>());
    }
    public void SetLaps(){
        AudioManager.Instance.PlaySound("Menu confirm platform",false,transform.position,false);
        CarreraManager.Instance.totalWaypointsInTrack=lapsOfStage*oneLapWP;
    }
}
