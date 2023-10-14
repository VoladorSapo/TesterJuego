using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MensajeObjeto : MonoBehaviour
{
    public IEnumerator setMessage(string _text,int _side,int _button,int type)
    {
            GetComponentInChildren<TMP_Text>().text = _text;
        //  type = _type;

        switch (_side)
        {
            case 0:
                GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-60, 0);
                break;
            case 1:
                GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 0);
                break;
        }
        if (_button != 0)
        {
            GetComponentInChildren<Image>().gameObject.AddComponent(typeof(Button));
        }
        GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().pivot = new Vector2(_side, 1);
        GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(_side, 1);
        GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(_side, 1);

        yield return new WaitForEndOfFrame();
        GetComponent<RectTransform>().sizeDelta = new Vector2(100, GetComponentsInChildren<RectTransform>()[1].sizeDelta.y);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInParent<RectTransform>());
        VerticalLayoutGroup layout = GetComponentInParent<VerticalLayoutGroup>();
        
        layout.SetLayoutVertical();
        layout.SetLayoutHorizontal();
       GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        print(GetComponentsInChildren<RectTransform>()[1].sizeDelta.y);

        if (Input.GetKeyDown(KeyCode.O))
        {
        }
        if (Input.GetKeyDown(KeyCode.I)){
            VerticalLayoutGroup layout = GetComponentInParent<VerticalLayoutGroup>();
            //layout.CalculateLayoutInputVertical();
            //layout.CalculateLayoutInputHorizontal();
            //layout.SetLayoutVertical();
            //layout.SetLayoutHorizontal();
            //LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInParent<RectTransform>());

        }
    }

  
}
