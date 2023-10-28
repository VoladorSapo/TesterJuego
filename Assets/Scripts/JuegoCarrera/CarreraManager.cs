using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class CarreraManager : MonoBehaviour
{
    public static CarreraManager Instance;

    //Menu Carreras
    public bool killMouse;

    //Calculo de Distancia
    Transform WaypointRoot;
    public int totalWaypointsInTrack=10;
    public int numberOfLaps;
    [HideInInspector] public int currentLap=1;


    [Header("Stages")]
    public string SelectedStage;
    public string NextStage;
    [HideInInspector] public Vector3 newPositionPlayer;

    [Header("Sprites de los Coches")]
    public List<SpritesCars> allSprites;
    private List<SpritesCars> availableSprites;
    [SerializeField] private int indexPlayerSprite;

    //Los coches activos
    public List<PositionRace> allPositions;
    UnityEngine.UI.Image[] allPositionsImages;


    [Header("Propiedades de las carreras")]
    public List<TileBase> NoDragTiles;
    [HideInInspector] public Tilemap NormalTilemap;
    [HideInInspector] public Tilemap GlitchedTilemap;
    public bool raceStarted=false;

    //Inicio
    void Awake()
    {
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }

        
    }

    void Start(){
        
    }
    public void Update(){
        if("MenuCar"==SceneManager.GetActiveScene().name && raceStarted){raceStarted=false;}
        if(raceStarted){
            UpdatePositionUI();
        }

        if(Input.GetKeyDown(KeyCode.U)){
            RaceFinished("PlayerCar",GameObject.Find("PlayerCar").GetComponent<PositionRace>());
        }

        
    }

    public void GoToRace(){
        Canvas canvas=GameObject.Find("MenuCanvas").GetComponent<Canvas>();
        canvas.enabled=false;
        
        SceneManager.LoadScene(SelectedStage);
        SceneManagement.Instance.narrativeParts.CarNarrative=1;
        
    }

    //SetRace
    public void SetRace(bool activateGlitchedMap){
        canWinRace=true;
        setVariables(activateGlitchedMap);
        setSprites();
        SetLaps();
        UpdatePositionUI();
        StartCoroutine(Countdown());
    }

    public void EndRace(){
        raceStarted=false;
    }

    [HideInInspector] public bool canWinRace=true;
    public void SetCanWin(bool b){canWinRace=b;}
    public void RaceFinished(string winner, PositionRace posRace){

        if(winner=="PlayerCar" && canWinRace){
        raceStarted=false;
        StartCoroutine(DebugWinner(winner));
        }

        allPositions.Remove(posRace);
        Debug.LogWarning(winner);
        minStart++;
    }

    void setVariables(bool activateGlitchedMap){

        if(totalWaypointsInTrack<35){totalWaypointsInTrack*=numberOfLaps;} //De testeo, no sera nada al final


        CarAI[] allAICarsInScene=GameObject.FindGameObjectsWithTag("AICar").Select(car=>car.GetComponent<CarAI>()).Where(ms=>ms!=null).ToArray();
        foreach(CarAI ai in allAICarsInScene){
            ai.CurrentWaypoint=GameObject.Find("Waypoint0").GetComponent<Waypoint>();
            ai.PreviousWaypoint=ai.CurrentWaypoint.PreviousWaypoints[0];
        }

        WaypointRoot=GameObject.Find("WaypointRoot")?.transform;

        allPositions.Clear();
        minStart=0;
        allPositions=GameObject.FindObjectsOfType<PositionRace>().ToList();
        foreach(PositionRace player in allPositions){
            player.WaypointsPassed=0;
            player.playerName=player.gameObject.name;
        }

        if(GameObject.Find("NormalTilemap")!=null)
            NormalTilemap=GameObject.Find("NormalTilemap").GetComponent<Tilemap>();
        
        EnableGlitchTilemap(activateGlitchedMap);

        Transform parentTransform=CamaraGlobal.Instance.attachedCanvas.carUI.transform.GetChild(1).transform;
        allPositionsImages = new UnityEngine.UI.Image[parentTransform.childCount];

        for(int i=0; i<parentTransform.childCount; i++){
            allPositionsImages[i]=parentTransform.GetChild(i).GetChild(0).GetComponent<UnityEngine.UI.Image>();
        }
        
    }

    public void EnableGlitchTilemap(bool activateGlitchedMap){
        if(GameObject.Find("GlitchedTilemap")!=null){
            GlitchedTilemap=GameObject.Find("GlitchedTilemap").GetComponent<Tilemap>();
            GlitchedTilemap.gameObject.GetComponent<TilemapRenderer>().enabled=activateGlitchedMap;
        }
    }
    void setSprites(){
        availableSprites=new List<SpritesCars>(allSprites);

        //Set Player sprite
        GameObject.FindObjectOfType<CarController>().GetComponent<SpriteRenderer>().sprite=availableSprites[indexPlayerSprite].spriteCar;
        GameObject.FindObjectOfType<CarController>().GetComponent<PositionRace>().spriteUI=availableSprites[indexPlayerSprite].spriteUI;
        availableSprites.RemoveAt(indexPlayerSprite);

        availableSprites=ShuffleList(availableSprites);
        SpriteRenderer[] allAICarsInScene=GameObject.FindGameObjectsWithTag("AICar").Select(car=>car.GetComponent<SpriteRenderer>()).Where(_spr=>_spr!=null).ToArray();
        int i=0;
        foreach(SpriteRenderer ai in allAICarsInScene){
            ai.sprite=availableSprites[i].spriteCar;
            ai.gameObject.GetComponent<PositionRace>().spriteUI=availableSprites[i].spriteUI;
            i++;
        }
    }

    void SetLaps(){
        currentLap=1;
        TextMeshProUGUI lapText=CamaraGlobal.Instance.attachedCanvas.carUI.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        lapText.text=currentLap+"/"+numberOfLaps;
    }
    IEnumerator Countdown(){
        CamaraGlobal.Instance.attachedCanvas.carUI.SetActive(true);
        int i=3;
        TextMeshProUGUI countText=CamaraGlobal.Instance.attachedCanvas.carUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        countText.gameObject.SetActive(true);

        AudioManager.Instance?.PlaySound("Race Countdown",false,Vector2.zero,true);
        while(i>0){
        countText.text=i.ToString();
        yield return new WaitForSeconds(1);
        i--;
        }
        countText.text="GO";
        yield return new WaitForSeconds(1);
        countText.gameObject.SetActive(false);

        BasicCar[] allCars = GameObject.FindObjectsOfType<BasicCar>();
        foreach(BasicCar car in allCars){
            car.canMove=true;
        }
        raceStarted=true;
        AudioManager.Instance?.ChangeMusicTo("",0,"Race Music",0.25f);
    }

    IEnumerator DebugWinner(string winner){
        CamaraGlobal.Instance.attachedCanvas.carUI.SetActive(true);
        TextMeshProUGUI text=CamaraGlobal.Instance.attachedCanvas.carUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        text.gameObject.SetActive(true);
        
        if(minStart==0){
        text.text=winner+" is the Winner!!";
        }else{
        text.text=winner+" Lost!!";
        }
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);

        CamaraGlobal.Instance.cameraFX.ApplyEffects(
            new List<TransitionData>()
            {
                new TransitionData("vram",false,true,true,0.5f,0),
                new TransitionData("bc",false,true,true,0.5f,0)
            }
        );
        
        yield return new WaitForSeconds(0.5f);

        if(GamesManager.Instance.unlockedCarStages+1<3)
        GamesManager.Instance.unlockedCarStages++;
        
        CamaraGlobal.Instance.attachedCanvas.carUI.SetActive(false);

        if(NextStage=="")
        SceneManager.LoadScene("MenuCar");
        else if(SceneIsInBuild(NextStage)){
        DontDestroyOnLoad(GameObject.Find("PlayerCar").gameObject);
        GameObject.Find("PlayerCar").transform.position=newPositionPlayer;
        SceneManager.LoadScene(NextStage);
        }

    }

    bool SceneIsInBuild(string sceneName){
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string scene = System.IO.Path.GetFileNameWithoutExtension(path);

            if (scene == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    public void StopRace(){
        CarController car=GameObject.FindObjectOfType<CarController>();
        allPositions.Clear();
    }

    //Otros
    public int minStart=0;
    void UpdatePositionUI(){
        OrderPositionsList();
        //Debug.Log(allPositions[0]);
        if(allPositions.Count>0)
        for(int i=0; i<allPositions.Count; i++){
            if(i+minStart<allPositionsImages.Length && allPositions[i]!=null)
            allPositionsImages[i+minStart].sprite=allPositions[i].gameObject.GetComponent<PositionRace>().spriteUI;
        }
        
    }
    public void OrderPositionsList(){
        allPositions.Sort((x,y)=>{
            int ret = y.WaypointsPassed.CompareTo(x.WaypointsPassed);
            if(ret==0){
                ret=x.DistanceToReachWaypoint.CompareTo(y.DistanceToReachWaypoint);
            }
            return ret;
        });
    }

    public void SetPlayerSprite(int index){
        indexPlayerSprite=index;
    }


    public void SetGlitchPlayer(){
        Sprite currSprite= GameObject.FindObjectOfType<CarController>().GetComponent<SpriteRenderer>().sprite;
        foreach(SpritesCars sc in allSprites){
            Debug.Log(sc.spriteCar==currSprite);
            if(sc.spriteCar==currSprite){
                GameObject.FindObjectOfType<CarController>().GetComponent<SpriteRenderer>().sprite=sc.spriteGlitchedCar;
                GameObject.FindObjectOfType<CarController>().GetComponent<PositionRace>().spriteUI=sc.spriteUIGlitched;
                UpdatePositionUI();
                GameObject.FindObjectOfType<CarController>().GlicthedCar();
                break;
            }
        }

    }

    public void SetReversedPlayer(){
        Sprite currSprite= GameObject.FindObjectOfType<CarController>().GetComponent<SpriteRenderer>().sprite;
        foreach(SpritesCars sc in allSprites){
            Debug.Log(sc.spriteCar==currSprite);
            if(sc.spriteCar==currSprite){
                GameObject.FindObjectOfType<CarController>().GetComponent<SpriteRenderer>().sprite=sc.spriteGlitchedCar;
                break;
            }
        }
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
    
    

    
}
