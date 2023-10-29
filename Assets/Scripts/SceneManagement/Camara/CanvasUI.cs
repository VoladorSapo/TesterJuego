using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] Image finalImage;
    [SerializeField] TextMeshProUGUI finalText;
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

        SceneManager.LoadScene("EndScene");
        for (int i = 0; i < creditsUI.transform.childCount-2; i++)
        {
            RollingText text= creditsUI.transform.GetChild(i).GetComponent<RollingText>();
            text.ScrollUp();
            
            yield return new WaitForSeconds(text.timeForNext);

            
        }

        StartCoroutine(Fade(1,4.5f));

        yield return new WaitForSeconds(1.5f);
        RollingText text1= creditsUI.transform.GetChild(creditsUI.transform.childCount-2).GetComponent<RollingText>();
        text1.ScrollUp();

        yield return new WaitForSeconds(4.75f);
        Debug.Log("Pa fuera");
        //Application.Quit();
    }

    //Application.Quit();

    IEnumerator Fade(float alpha, float timeFades){

        Color currentColor = finalImage.color;
        Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        float elapsedTime = 0f;

        while (elapsedTime < timeFades)
        {
            finalImage.color = Color.Lerp(currentColor, targetColor, elapsedTime / timeFades);
            elapsedTime += Time.deltaTime;
            yield return null;
        }



    }
    
}
