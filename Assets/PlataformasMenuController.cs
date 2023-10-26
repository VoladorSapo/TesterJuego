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
        SceneManager.LoadScene("Nivel 1");
    }
    public void TryQuit()
    {
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
