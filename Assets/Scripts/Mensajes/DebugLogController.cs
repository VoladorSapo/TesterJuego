using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugLogController : MonoBehaviour
{
  [SerializeField]  public GameObject Content;
    [SerializeField] GameObject LogPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddLog("Te amordido un perro");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddLog("Sueor" + '\n' + "Boblon");
        }
    }

    public void AddLog(string text)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GameObject newlog = Instantiate(LogPrefab, Content.transform.position,Quaternion.identity);
        newlog.transform.SetParent(Content.transform);
        newlog.GetComponentInChildren<TMP_Text>().text = text;
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
