using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClass
{
    public int Character, anim;
    public string text,nombre;

    public DialogueClass (string _text,string _name,int _character,int _anim)
    {
        Character = _character;
        anim = _anim;
        text = _text;
        nombre = _name;
    }
    // Start is called before the first frame update
}
