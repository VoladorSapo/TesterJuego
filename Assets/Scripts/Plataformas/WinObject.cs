using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinObject : MonoBehaviour
{
    Animator anim;
 [SerializeField]   GameObject WinScreen;
   [SerializeField] bool canNext;
    [SerializeField] string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            WinScreen = GameObject.Find("NextLevelScreen");
            WinScreen.SetActive(true);
            if (canNext)
            {
                WinScreen.GetComponent<CanvasGroup>().alpha = 1;
                WinScreen.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { LoadScene(); });
            }
            else
            {
                WinScreen.GetComponent<CanvasGroup>().alpha = 0;
                WinScreen.GetComponentsInChildren<Button>()[0].interactable = false;
            }
        }
    }
    public void LoadScene()
    {
        WinScreen.GetComponent<CanvasGroup>().alpha = 0;
        WinScreen.GetComponentsInChildren<Button>()[0].interactable = false;
        SceneManager.LoadScene(nextScene);
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SetEvents();
       
    }
    private void OnDestroy()
    {
        PauseController.Instance?.UnSetPausedEvents(Pause, Unpause);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
    }
    public void Pause()
    {
        anim.speed = 0;
    }

    public void Unpause()
    {
        anim.speed = 1;
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }

}
