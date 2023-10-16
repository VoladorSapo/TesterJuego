using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageClass
{
  public  int side;
    public int type;
    public string text;
   public int time;
    public bool waitTimeFixed; //Si es false se espera dependiendo del numero de letras + time, si no el numero q se diga
    public string isButton;
    public MessageClass(string _text, int _type, int _side,int _time, bool _timefixed, string _isbutton)
    {
        side = _side;
        text = _text;
        time = _time;
        waitTimeFixed = _timefixed;
        isButton = _isbutton;
        type = _type;
    }
}
