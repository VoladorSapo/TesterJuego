using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageDictionary :MonoBehaviour, ISerializationCallbackReceiver

{
    [SerializeField] DictionaryScriptableObject Data;
    public static ImageDictionary Instance;
    [SerializeField] private static Dictionary<string, Sprite> imagelist = new Dictionary<string, Sprite>();
    [SerializeField] private List<string> keys = new List<string>();
    [SerializeField] private List<Sprite> values = new List<Sprite>();

    public bool modifyvalues;
    private void Awake()
    {
        print("soy el dictionary");
        if (Instance == null)
        {
            for (int i = 0; i < Mathf.Min(Data.Keys.Count, Data.Values.Count); i++)
            {
                imagelist.Add(Data.Keys[i], Data.Values[i]);
            }
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



  public Sprite getImage(string key)
    {
        if (imagelist.ContainsKey(key))
        {
            return imagelist[key];
        }

        else
        {
            print("diablo nen");
            return null;
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        if (!modifyvalues)
        {
            if (Data != null)
            {
                keys.Clear();
                values.Clear();
                for (int i = 0; i < Mathf.Min(Data.Keys.Count, Data.Values.Count); i++)
                {
                    keys.Add(Data.Keys[i]);
                    values.Add(Data.Values[i]);
                }
            }
        }
    }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
    }
    public void DeserializeDictionary()
    {
        Debug.Log("DESERIALIZATION");
        imagelist = new Dictionary<string, Sprite>();
        Data.Keys.Clear();
        Data.Values.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            Data.Keys.Add(keys[i]);
            Data.Values.Add(values[i]);
            imagelist.Add(keys[i], values[i]);
        }
        modifyvalues = false;
    }

    public void PrintDictionary()
    {
        foreach (var pair in imagelist)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }
    }
}
