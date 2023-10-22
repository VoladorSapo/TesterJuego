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
    [SerializeField] int nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            WinScreen.SetActive(true);
            if (canNext)
            {
                WinScreen.GetComponentsInChildren<Button>()[0].interactable = true;
                WinScreen.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { LoadScene(); });
            }
            else
            {
                WinScreen.GetComponentsInChildren<Button>()[0].interactable = false;
            }
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    // Start is called before the first frame update
    void Start()
    {
        WinScreen = GameObject.Find("NextLevelScreen");
        WinScreen.SetActive(false);
        anim = GetComponent<Animator>();
        SetEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
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
