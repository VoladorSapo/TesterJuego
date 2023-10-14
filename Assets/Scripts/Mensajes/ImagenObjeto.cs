using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImagenObjeto : MonoBehaviour
{
    public IEnumerator SetImage(string _text, int _side, int _button, int type)
    {
        print("jooo");
        switch (_side)
        {
            case 0:
                GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-60, 0);
                break;
            case 1:
                GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 0);
                break;
        }
        GetComponentInChildren<Image>().sprite = ImageDictionary.Instance.getImage(_text);

        yield return new WaitForEndOfFrame();
        GetComponent<RectTransform>().sizeDelta = new Vector2(100, GetComponentsInChildren<RectTransform>()[1].sizeDelta.y);
        GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;

    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
