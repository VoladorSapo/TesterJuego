using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MensajeObjeto : MonoBehaviour
{
    [SerializeField] int margin;
    [SerializeField] int limit;
    [SerializeField] int divide;
    public IEnumerator setMessage(string _text,int _side,string _button,int type)
    {
            GetComponentInChildren<TMP_Text>().text = _text;
       //print( GetComponentInChildren<TMP_Text>().preferredWidth + margin);
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
        int x =1;
        while (GetComponentInChildren<TMP_Text>().preferredWidth + margin > GetComponentInParent<ScrollRect>().gameObject.GetComponent<RectTransform>().sizeDelta.x && x<=limit)
        {
            string a = GetComponentInChildren<TMP_Text>().text;
            print("jajajajajaja" + _text);
            GetComponentInChildren<TMP_Text>().text = ReturnDividedString(_text,x);
            x++;
        }

        if (!string.IsNullOrWhiteSpace(_button) && _button != "None")
        {
            GetComponentInChildren<Image>().gameObject.AddComponent(typeof(Button));
            GetComponentInParent<MessageAdder>().AddButtonClick(GetComponentInChildren<Button>(), _button);
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
 string ReturnDividedString(string baseString,int divided)
    {
        if (GetComponentInParent<ScrollRect>().gameObject.GetComponent<RectTransform>().sizeDelta.x < GetComponentInChildren<TMP_Text>().preferredWidth + margin)
        {
            //print(GetComponentInParent<ScrollRect>().gameObject.GetComponent<RectTransform>().sizeDelta.x + "heeelp" + baseString);
            //print(GetComponentInChildren<TMP_Text>().preferredWidth + margin + "heeelp");
            string[] strings = baseString.Split("<br>");
            string newString = "";
            foreach (string item in strings)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    GetComponentInChildren<TMP_Text>().text = item;

                    if (GetComponentInParent<ScrollRect>().gameObject.GetComponent<RectTransform>().sizeDelta.x < GetComponentInChildren<TMP_Text>().preferredWidth + margin)
                    {
                        print("jdrr" +" "+item+ "/////" + baseString);
                        print(item.Length);
                        print(item);
                        string newItem = item;
                        for (int i = divided; i > 1; i--)
                        {                            
                            int a = newItem.IndexOf(' ', newItem.Length / i);
                            print(a + " " +newItem);
                            if (a > 0)
                            {
                                newString += newItem.Substring(0, a) + "<br>";
                                newItem = newItem.Substring(a+1);
                            }
                            else
                            {
                                break;
                            }
                        }
                        newString += newItem;

                    }
                    else
                    {
                        newString += item + "<br>";
                    }
                }
            }
            return (newString);
        }
        return (baseString);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            print(GetComponentInChildren<TMP_Text>().preferredWidth + margin);
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
