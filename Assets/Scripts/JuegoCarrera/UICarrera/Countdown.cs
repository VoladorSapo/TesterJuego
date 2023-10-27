using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
   [SerializeField] Material matCopy;

    Material mat;
    void Awake(){
        mat = Instantiate<Material>(matCopy);
    }
}
