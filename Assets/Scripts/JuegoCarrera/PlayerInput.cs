using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CarController _carController;
    [SerializeField] float _backTime;
    [SerializeField] float _backTimer;
    [SerializeField] float _backAmmount;
    private void Awake()
    {
        _carController = GetComponent<CarController>();
    }

    private void Start()
    {

    }
    private void Update()
    {

        Vector2 input = Vector2.zero;

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        _carController.SetInputVector(input);
    }

    public void GoBack(){
        _backTimer=_backTime;
    }
}
