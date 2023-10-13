using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{

    public ShaderEffect_BleedingColors shdr_bc;
    public ShaderEffect_CorruptedVram shdr_vram;
    public ShaderEffect_CRT shdr_crt;
    public ShaderEffect_Unsync shdr_unsync;

    void Awake(){
        
    }

    void Update(){
        
    }

    public void ActivateTemporaryEffect(string nameFX, bool fluctuate, float time){
        StartCoroutine(TemporaryFX(nameFX,fluctuate,time));
    }
    IEnumerator TemporaryFX(string nameFX, bool fluctuate, float time){
        switch(nameFX){
            case "bc": shdr_bc.enabled=true; shdr_bc.Fluctuate=fluctuate; break;
            case "vram": shdr_vram.enabled=true; shdr_vram.Fluctuate=fluctuate; break;
            case "crt": shdr_crt.enabled=true; shdr_crt.Fluctuate=fluctuate; break;
            case "unsync": shdr_unsync.enabled=true; break;
            default: break;
        }

        float remainingTime=time;

        while(remainingTime>0){
            yield return null;
            remainingTime-=Time.deltaTime;
        }

        switch(nameFX){
            case "bc": shdr_bc.enabled=false; break;
            case "vram": shdr_vram.enabled=false; break;
            case "crt": shdr_crt.enabled=false; break;
            case "unsync": shdr_unsync.enabled=false; break;
            default: break;
        }
    }

    public void ActivateEffect(string nameFX, bool fluctuate, bool activate){
        switch(nameFX){
            case "bc": shdr_bc.enabled=activate; shdr_bc.Fluctuate=fluctuate; break;
            case "vram": shdr_vram.enabled=activate; break;
            case "crt": shdr_crt.enabled=activate; break;
            case "unsync": shdr_unsync.enabled=activate; break;
            default: break;
        }
    }


}
