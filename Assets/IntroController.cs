using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroController : MonoBehaviour
{
   [SerializeField] DialogueController dialog;
    [SerializeField] GlobalWarpPoint warp;
    // Start is called before the first frame update
    void Start()
    {
        IEnumerator cour = LateStart();
        StartCoroutine(cour);
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        dialog.getConversation("cinematic1");
        dialog.setEndConversation(endScene);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void endScene()
    {
        EventGallery.Instance.ClearEffects();
        dialog.unsetEndConversation(endScene);
        warp.DoTransition();
       // SceneManager.LoadScene("Bosque 1");
    }
}
