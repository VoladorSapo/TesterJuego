using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageButton : ButtonSelection
{
    [SerializeField] private string stageName;
    [SerializeField] private int lapsOfStage;
    [SerializeField] private int oneLapWP;
    [SerializeField] private int narrativeLocalChange;

    [SerializeField] private string actionName;
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

        SetPauseEvents();
    }
    public void SetLaps(){
        AudioManager.Instance.PlaySound("Menu confirm platform",false,transform.position,false);
        CarreraManager.Instance.SelectedStage=stageName;
        CarreraManager.Instance.totalWaypointsInTrack=lapsOfStage*oneLapWP;
        SceneManagement.Instance.SetNextScene(actionName);
    }

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
