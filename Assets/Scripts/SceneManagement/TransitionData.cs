using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransitionData
{
    public string nameFX;
    public bool fluctuate;
    public bool isTemporary;
    public bool activate;
    public float time;
    public float fluctuateValue;

    public TransitionData(string name, bool fluc, bool isTemp, bool act, float time, float value){
        nameFX=name;
        fluctuate=fluc;
        isTemporary=isTemp;
        activate=act;
        this.time=time;
        fluctuateValue=value;
    }


}
