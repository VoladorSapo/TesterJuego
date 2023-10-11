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

    //Menu Carreras
    public bool killMouse;

    //Calculo de Distancia
    Transform WaypointRoot;
    public int totalWaypointsInTrack=10;
    [HideInInspector] public int numberOfLaps;


    [Header("Stages")]
    public string SelectedStage;

    [Header("Sprites de los Coches")]
    public List<Sprite> allSprites;
    private List<Sprite> availableSprites;
    [SerializeField] private int indexPlayerSprite;

    //Los coches activos
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
        
    }
    public void Update(){
        if(killMouse)
        KillMouseInputs();
    }

    public void GoToRace(){
        Canvas canvas=FindObjectOfType<Canvas>();
        canvas.enabled=false;
        
        SceneManager.LoadScene(SelectedStage);
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

        WaypointRoot=GameObject.Find("WaypointRoot")?.transform;

        allPositions=GameObject.FindObjectsOfType<PositionRace>().ToList();
        foreach(PositionRace player in allPositions){
            player.WaypointsPassed=0;
            player.playerName=player.gameObject.name;
        }

        if(GameObject.Find("NormalTilemap")!=null)
            NormalTilemap=GameObject.Find("NormalTilemap").GetComponent<Tilemap>();
        
        if(GameObject.Find("GlitchedTilemap")!=null){
            GlitchedTilemap=GameObject.Find("GlitchedTilemap").GetComponent<Tilemap>();
            GlitchedTilemap.gameObject.GetComponent<TilemapRenderer>().enabled=false;
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
        if(EventSystem.current?.currentSelectedGameObject==null){
            if(lastSelected!=null)
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }else if(EventSystem.current.currentSelectedGameObject!=null){
            lastSelected=EventSystem.current.currentSelectedGameObject;
        }
    }

    
}
