using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zeldaNPC : MonoBehaviour
{
    [SerializeField] private string conversationKey;
    public void Say(){
        DialogueController.Instance.getConversation(conversationKey);
    }
}
