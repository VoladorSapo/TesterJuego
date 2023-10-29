using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionCollider : MonoBehaviour
{
    [SerializeField] string condition;
    // Start is called before the first frame update
    void Start()
    {
            GetComponent<Collider2D>().enabled = SceneManagement.Instance.actionName == condition ? true:false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
