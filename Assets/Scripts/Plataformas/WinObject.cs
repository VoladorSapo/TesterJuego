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
    bool hasTouched;
    [SerializeField] string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hasTouched = true;
            WinScreen = GameObject.Find("NextLevelScreen");
            if (canNext)
            {
            }
            else
            {
               // WinScreen.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
    }
    public void LoadScreen()
    {
        WinScreen.SetActive(true);

        WinScreen.GetComponent<CanvasGroup>().alpha = 1;

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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("me voy a suicidar y sera tu culpa edu");
            if(WinScreen != null && WinScreen.GetComponent<CanvasGroup>().alpha == 1)
            {
                WinScreen.GetComponent<CanvasGroup>().alpha = 0;
                WinScreen.GetComponentsInChildren<Button>()[0].interactable = false;
                GameObject obj = GameObject.Find("Nivel2Warp");
                if (obj)
                {
                    obj.GetComponent<GlobalWarpPoint>().DoTransition();
                }
            }
        }
    }
    public void Pause()
    {
        anim.speed = 0;
    }

    public void Unpause()
    {
        anim.speed = 1;
        if(hasTouched && WinScreen.GetComponent<CanvasGroup>().alpha== 0)
        {
            LoadScreen();
        }
    }

    public void SetEvents()
    {
        PauseController.Instance?.SetPausedEvents(Pause, Unpause);
    }

}
