using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CamaraGlobal.Instance.attachedCanvas.carUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            FindObjectOfType<GlobalWarpPoint>().DoTransition();
        }
    }
}
