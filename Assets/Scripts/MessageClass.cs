using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageClass
{
  public  int type;
    public string text;
   public int time;
    public bool waitTimeFixed; //Si es false se espera dependiendo del numero de letras + time, si no el numero q se diga

    public MessageClass(int _type,int _time, string _text,bool _timefixed)
    {
        type = _type;
        text = _text;
        time = _time;
        waitTimeFixed = _timefixed;
    }
}
