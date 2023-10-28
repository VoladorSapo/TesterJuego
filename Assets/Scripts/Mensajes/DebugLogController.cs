using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DebugLogController : MonoBehaviour
{
  [SerializeField]  public GameObject Content;
   public static DebugLogController Instance;
    [SerializeField] GameObject LogPrefab;
    [SerializeField] TriggerStoryEvent  trigger;
    [SerializeField] float dissapearWait;
    [SerializeField] float waited;
    [SerializeField] float dissapearSpeed;
    [SerializeField] bool dissapearing;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GameObject triggerG = GameObject.Find("TriggerLog");
        if (triggerG != null)
        {
            trigger = triggerG.GetComponent<TriggerStoryEvent>();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        waited += Time.deltaTime;
        if(waited > dissapearWait)
        {
            dissapearing = true;
        }
        if (dissapearing)
        {
            GetComponent<CanvasGroup>().alpha-= dissapearSpeed;
        }
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    AddLog("Te amordido un perro");
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    AddLog("Sueor" + '\n' + "Boblon");
        //}

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        GameObject triggerG = GameObject.Find("TriggerBug");
        if (triggerG != null )
        {
            trigger = triggerG.GetComponent<TriggerStoryEvent>();
        }
    }
    public void AddLog(string text)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GameObject newlog = Instantiate(LogPrefab, Content.transform.position,Quaternion.identity);
        newlog.transform.SetParent(Content.transform);
        newlog.GetComponentInChildren<TMP_Text>().text = text;
        waited = 0;
    }
    public string[] GetLogs(string key)
    {
        List<string> logs = new List<string>();
        int i = 0;
        string log = DialogueList.Instance.getLog(key + "_" + i);
        while (!string.IsNullOrWhiteSpace(log))
        {
            print("waka");
            i++;
            logs.Add(log);
            log = DialogueList.Instance.getLog(key + "_" + i);
        }
        print(logs.Count + "log length");
        return logs.ToArray();
    }
    public IEnumerator AddLogs(string[] strings)
    {
        print(strings.Length);
        foreach (string item in strings)
        {
            AddLog(item);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void checkKey(string key)
    {
        if (trigger != null && !trigger.HasTriggered && System.Array.Exists(trigger.summonkeys, element => element == key))
        {
            trigger.TriggerEvent();
        }
    }
    public void DeleteLogs()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        foreach(Transform child in Content.transform)
        {
            foreach (Transform children in child.transform)
            {
                GameObject.Destroy(children.gameObject);
            }
            GameObject.Destroy(child.gameObject);
        }
    }
}
