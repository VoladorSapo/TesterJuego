using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Nuevo Diccionario",menuName = "DataObjects/Diccionario")]
public class DictionaryScriptableObject : ScriptableObject
{
    [SerializeField] private List<string> keys = new List<string>();
    [SerializeField] private List<Sprite> values = new List<Sprite>();


    public List<string> Keys { get => keys; set => keys = value; }
    public List<Sprite> Values { get => values; set => values = value; }
}
