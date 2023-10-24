using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        IEnumerator cour = LateStart();
        StartCoroutine(cour);
    }
    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        DialogueController.Instance.getConversation("dialog");
        DialogueController.Instance.setEndConversation(endScene);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void endScene()
    {
        DialogueController.Instance.unsetEndConversation(endScene);
        SceneManager.LoadScene("protoZelda");
    }
}
