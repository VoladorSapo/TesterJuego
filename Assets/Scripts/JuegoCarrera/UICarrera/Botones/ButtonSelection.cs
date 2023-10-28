
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelection : MonoBehaviour, ISelectHandler, IPauseSystem
{   
    [Header("Display Frames")]
    [SerializeField] protected UnityEngine.UI.Image imageDisplay;
    [SerializeField] protected TextMeshProUGUI textDisplay;
    
    [Header("Button Properties")]
    [SerializeField] public Sprite thisButtonImage;
    [SerializeField] public string thisDescription;

    protected virtual void Start(){

    }

    protected virtual void Update(){
        
    }
    public virtual void OnSelect(BaseEventData eventData)
    {
        imageDisplay.sprite=thisButtonImage;
        textDisplay.text=thisDescription;
        AudioManager.Instance.PlaySound("Menu confirm pixel",false,transform.position,false);
    }

    public virtual void Pause()
    {
        
    }

    public virtual void Unpause()
    {
        
    }

    public virtual void SetPauseEvents()
    {
        
    }
}
