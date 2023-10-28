using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField]string key;
    [SerializeField] int num;
    [SerializeField] float[] times;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<SistemaPistas>().NextPuzzle(num, key, times);
        }
    }
}
