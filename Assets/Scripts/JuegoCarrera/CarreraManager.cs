using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CarreraManager : MonoBehaviour
{
    public static CarreraManager Instance;

    //Calculo de Distancia
    Transform WaypointRoot;
    [HideInInspector] public int totalWaypointsInTrack=10;
    [HideInInspector] public int numberOfLaps;


    [Header("Stages")]
    public List<string> allStages;
    //[HideInInspector] 
    public int indexSelectedStage;

    [Header("Sprites de los Coches")]
    public List<Sprite> allSprites;
    private List<Sprite> availableSprites;
    [SerializeField] private int indexPlayerSprite;

    [Header("Los Coches Activos")]
    public List<PositionRace> allPositions;

    //Inicio
    bool isGameSet=false;
    void Awake()
    {
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this);
        }
    }

    public void Update(){
        if(!allStages.Contains(SceneManager.GetActiveScene().name)){
            isGameSet=false;
            KillMouseInputs();
        }
        if(allStages.Contains(SceneManager.GetActiveScene().name) && !isGameSet){
            isGameSet=true;
            SetRace();
        }
    }

    public void PlayRace(){
        //SceneManager.LoadScene(allStages[indexSelectedStage]);
    }

    //SetRace
    public void SetRace(){
        setVariables();
        setNumberOfWaypoints();
        setSprites();
    }

    void setVariables(){
        CarAI[] allAICarsInScene=GameObject.FindGameObjectsWithTag("AICar").Select(car=>car.GetComponent<CarAI>()).Where(ms=>ms!=null).ToArray();
        foreach(CarAI ai in allAICarsInScene){
            ai.CurrentWaypoint=GameObject.Find("Waypoint0").GetComponent<Waypoint>();
            ai.PreviousWaypoint=ai.CurrentWaypoint.PreviousWaypoints[0];
        }

        WaypointRoot=GameObject.Find("WaypointRoot").transform;

        allPositions=GameObject.FindObjectsOfType<PositionRace>().ToList();
        foreach(PositionRace player in allPositions){
            player.WaypointsPassed=0;
            player.playerName=player.gameObject.name;
        }
    }
    void setSprites(){
        availableSprites=new List<Sprite>(allSprites);

        //Set Player sprite
        GameObject.FindObjectOfType<CarController>().GetComponent<SpriteRenderer>().sprite=availableSprites[indexPlayerSprite];
        availableSprites.RemoveAt(indexPlayerSprite);

        availableSprites=ShuffleList(availableSprites);
        SpriteRenderer[] allAICarsInScene=GameObject.FindGameObjectsWithTag("AICar").Select(car=>car.GetComponent<SpriteRenderer>()).Where(_spr=>_spr!=null).ToArray();
        int i=0;
        foreach(SpriteRenderer ai in allAICarsInScene){
            ai.sprite=availableSprites[i];
            i++;
        }
    }
    public void setNumberOfWaypoints(){
        
        totalWaypointsInTrack=numberOfLaps*WaypointRoot.childCount;
    }


    //Otros
    public void OrderPositionsList(){
        allPositions=allPositions.OrderByDescending(wp=>wp.WaypointsPassed).ToList();
    }

    public void SetPlayerSprite(int index){
        indexPlayerSprite=index;
    }
    List<T> ShuffleList<T>(List<T> myList)
    {
        int n = myList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T temp = myList[k];
            myList[k] = myList[n];
            myList[n] = temp;
        }
        return myList;
    }
    
    GameObject lastSelected;
    void KillMouseInputs(){
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
        if(EventSystem.current.currentSelectedGameObject==null){
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }else{
            lastSelected=EventSystem.current.currentSelectedGameObject;
        }
    }

    
}
