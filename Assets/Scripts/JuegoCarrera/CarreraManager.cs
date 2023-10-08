using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class CarreraManager : MonoBehaviour
{
    public static CarreraManager Instance;

    //Calculo de Distancia
    Transform WaypointRoot;
    [HideInInspector] public int totalWaypointsInTrack=10;
    [HideInInspector] public int numberOfLaps;


    [Header("Stages")]
    
    //[HideInInspector] 
    public int indexSelectedStage;

    [Header("Sprites de los Coches")]
    public List<Sprite> allSprites;
    private List<Sprite> availableSprites;
    [SerializeField] private int indexPlayerSprite;

    [Header("Los Coches Activos")]
    public List<PositionRace> allPositions;


    [Header("Propiedades de las carreras")]
    public List<Tile> NoDragTiles;
    [HideInInspector] public Tilemap NormalTilemap;
    [HideInInspector] public Tilemap GlitchedTilemap;

    //Inicio
    void Awake()
    {
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this);
        }

        
    }

    void Start(){
        if(GameObject.Find("NormalTilemap")!=null)
            NormalTilemap=GameObject.Find("NormalTilemap").GetComponent<Tilemap>();
        
        if(GameObject.Find("GlitchedTilemap")!=null)
            GlitchedTilemap=GameObject.Find("GlitchedTilemap").GetComponent<Tilemap>();
    }
    public void Update(){
        
    }

    public void GoToRace(){
        //SceneManager.LoadScene(allStages[indexSelectedStage].name);
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
    public void KillMouseInputs(){
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
        if(Event.current!= null && EventSystem.current.currentSelectedGameObject==null){
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }else if(Event.current!= null){
            lastSelected=EventSystem.current.currentSelectedGameObject;
        }
    }

    
}
