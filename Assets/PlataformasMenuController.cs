using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlataformasMenuController : MonoBehaviour
{

   [SerializeField] GameObject ConfirmBox;
    // Start is called before the first frame update
    void Start()
    {
        ConfirmBox.SetActive(false);
    }

  public  void StartGame()
    {
        AudioManager.Instance.PlaySound("Menu confirm platform",false,this.transform.position,true);
        SceneManager.LoadScene("Nivel 1");
    }
    public void TryQuit()
    {
        AudioManager.Instance.PlaySound("Back UI",false,this.transform.position,true);
        ConfirmBox.SetActive(true);
    }
    public void NoQuit()
    {
        ConfirmBox.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
