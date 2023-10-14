using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClass : MonoBehaviour
{
    public int Character, anim;
    public string text;

    public DialogueClass (int _character,int _anim,string _text)
    {
        Character = _character;
        anim = _anim;
        text = _text;
    }
    // Start is called before the first frame update
}
