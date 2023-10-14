using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueList : MonoBehaviour
{
    TextAsset text;

    // Start is called before the first frame update
    void Start()
    {
       string[] strings = text.text.Split(';');

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
