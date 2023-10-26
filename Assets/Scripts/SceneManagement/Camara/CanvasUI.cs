using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    public GameObject platformUI;
    public GameObject carUI;
    public GameObject zeldaUI;
    public GameObject creditsUI;
    [SerializeField] float speedFadeIn;
    void Update(){
        /*if(Input.GetKeyDown(KeyCode.I)){
            Debug.Log(CamaraGlobal.Instance.attachedCanvas.carUI.transform.GetChild(1).transform.childCount);
        }*/
    }

    public void StartCredits(){
        StartCoroutine(CreditsRoll());
    }
    IEnumerator CreditsRoll(){
        creditsUI.gameObject.SetActive(true);
        Image blackImage=creditsUI.GetComponent<Image>();
        float a=0;
        blackImage.color=new Color(0,0,0,a);
        while(blackImage.color.a<1){
            a+=Time.deltaTime*speedFadeIn;
            blackImage.color=new Color(0,0,0,a);
            yield return null;
        }

        SceneManager.LoadSceneAsync("EndScene");
        for (int i = 0; i < creditsUI.transform.childCount; i++)
        {
            RollingText text= creditsUI.transform.GetChild(i).GetComponent<RollingText>();
            text.ScrollUp();
            
            yield return new WaitForSeconds(text.timeForNext);

            // If you need to access the GameObject, use 'child.gameObject'.
            // Example: Debug.Log("Child Name: " + child.gameObject.name);
        }

    }
}
