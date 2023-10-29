using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageButton : ButtonSelection
{
    [SerializeField] private SlideUI slideUI;
    [SerializeField] private string stageName;
    [SerializeField] private int lapsOfStage;
    [SerializeField] private int oneLapWP;

    [SerializeField] private string actionName;
    [SerializeField] private int buttonID;

    protected override void Update(){

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
        if(GamesManager.Instance.unlockedCarStages<buttonID || GamesManager.Instance.unlockedCarStages!=buttonID){return;}

        AudioManager.Instance.PlaySound("Menu confirm platform",false,Vector2.zero,false);
        CarreraManager.Instance.SelectedStage=stageName;
        CarreraManager.Instance.totalWaypointsInTrack=lapsOfStage*oneLapWP;
        SceneManagement.Instance.SetNextScene(actionName);
        slideUI.SlideLeft();
    }

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
        if(EventSystem.current?.currentSelectedGameObject!=null && EventSystem.current?.currentSelectedGameObject.name==this.gameObject.name){
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
