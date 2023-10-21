using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GamesManager : MonoBehaviour
{
    public static GamesManager Instance;

    [Header("Car Game")]
    public int unlockedCarStages=0;

    void Awake(){
        if(Instance==null){
            Instance=this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void CarButtonStage(int buttonID, Button button){

        Button[] stageButtons=GameObject.FindObjectsOfType<Button>().Where(button=>button.GetComponent<StageButton>()!=null).ToArray();
        Navigation nav=button.navigation;

        switch(buttonID){
            case 0: if(unlockedCarStages>0){nav.selectOnRight=stageButtons[3];} if(unlockedCarStages>2){nav.selectOnLeft=stageButtons[2];} button.navigation=nav; break;
            case 1: if(unlockedCarStages>1){nav.selectOnRight=stageButtons[1];} if(unlockedCarStages>0){nav.selectOnLeft=stageButtons[0];} button.navigation=nav; break;
            case 2: if(unlockedCarStages>2){nav.selectOnRight=stageButtons[2];} if(unlockedCarStages>1){nav.selectOnLeft=stageButtons[3];} button.navigation=nav; break;
        }

    }
}
