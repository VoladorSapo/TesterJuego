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
    public ShaderEffect_Mosaic shdr_mos;
    public ShaderEffect_Pixelation shdr_pix;
    public ShaderEffect_InvertColorBits shdr_invc;
    public ShaderEffect_Tint shdr_tint;
    public ShaderEffect_Limiter shdr_lim;
    public ShaderEffect_Posterize shdr_pos;


    void Awake(){
        
    }

    void Update(){
        
    }

    public void ApplyEffects(List<TransitionData> data){
        foreach(TransitionData t in data){
            if(!t.isTemporary){
        
            ActivateEffect(t.nameFX,t.fluctuate,t.activate);
            }
            else{
                if(t.fluctuateValue<=0)
                ActivateTemporaryEffect(t.nameFX,t.fluctuate,t.time);
                else
                ActivateTemporaryEffect(t.nameFX,t.fluctuate,t.time,t.fluctuateValue);
            }
        }
    }
    public void ActivateTemporaryEffect(string nameFX, bool fluctuate, float time){
        StartCoroutine(TemporaryFX(nameFX,fluctuate,time));
    }

    public void ActivateTemporaryEffect(string nameFX, bool fluctuate, float time, float fluctuateValue){
        StartCoroutine(TemporaryFX(nameFX,fluctuate,time,fluctuateValue));
    }
    IEnumerator TemporaryFX(string nameFX, bool fluctuate, float time){
        switch(nameFX){
            case "bc": shdr_bc.enabled=true; shdr_bc.Fluctuate=fluctuate; break;
            case "vram": shdr_vram.enabled=true; shdr_vram.Fluctuate=fluctuate; break;
            case "crt": shdr_crt.enabled=true; shdr_crt.Fluctuate=fluctuate; break;
            case "unsync": shdr_unsync.enabled=true; break;
            case "mos": shdr_mos.enabled=true; shdr_mos.Fluctuate=fluctuate; break;
            case "pix": shdr_pix.enabled=true; break;
            case "invc": shdr_invc.enabled=true; shdr_invc.Fluctuate=fluctuate; break;
            case "tint": shdr_tint.enabled=true; shdr_tint.Fluctuate=fluctuate; break;
            case "lim": shdr_lim.enabled=true; break;
            case "pos": shdr_pos.enabled=true; break;
            case "glitchMap": CarreraManager.Instance?.EnableGlitchTilemap(true); break;
            default: break;
        }

        float remainingTime=time;

        while(remainingTime>0){
            yield return null;
            remainingTime-=Time.deltaTime;
        }

        fluctuate=false;
        switch(nameFX){
            case "bc": shdr_bc.enabled=false; shdr_bc.Fluctuate=fluctuate; break;
            case "vram": shdr_vram.enabled=false; shdr_vram.Fluctuate=fluctuate; break;
            case "crt": shdr_crt.enabled=false; shdr_crt.Fluctuate=fluctuate; break;
            case "unsync": shdr_unsync.enabled=false; break;
            case "mos": shdr_mos.enabled=false; shdr_mos.Fluctuate=fluctuate; break;
            case "pix": shdr_pix.enabled=false; break;
            case "invc": shdr_invc.enabled=false; shdr_invc.Fluctuate=fluctuate; break;
            case "tint": shdr_tint.enabled=false; shdr_tint.Fluctuate=fluctuate; break;
            case "lim": shdr_lim.enabled=false; break;
            case "pos": shdr_pos.enabled=false; break;
            case "glitchMap": CarreraManager.Instance?.EnableGlitchTilemap(false); break;
            default: break;
        }
    }

    IEnumerator TemporaryFX(string nameFX, bool fluctuate, float time, float fluctuateValue){
        switch(nameFX){
            case "bc": shdr_bc.enabled=true; shdr_bc.Fluctuate=fluctuate; shdr_bc.shift=fluctuateValue; break;
            case "vram": shdr_vram.enabled=true; shdr_vram.Fluctuate=fluctuate; shdr_vram.shift=fluctuateValue; break;
            case "crt": shdr_crt.enabled=true; shdr_crt.Fluctuate=fluctuate; break;
            case "unsync": shdr_unsync.enabled=true; break;
            case "mos": shdr_mos.enabled=true; shdr_mos.Fluctuate=fluctuate; break;
            case "pix": shdr_pix.enabled=true; break;
            case "invc": shdr_invc.enabled=true; shdr_invc.Fluctuate=fluctuate; break;
            case "tint": shdr_tint.enabled=true; shdr_tint.Fluctuate=fluctuate; break;
            case "lim": shdr_lim.enabled=true; break;
            case "pos": shdr_pos.enabled=true; break;
            case "glitchMap": CarreraManager.Instance?.EnableGlitchTilemap(true); break;
            default: break;
        }

        float remainingTime=time;

        while(remainingTime>0){
            yield return null;
            remainingTime-=Time.deltaTime;
        }

        fluctuate=false;
        switch(nameFX){
            case "bc": shdr_bc.enabled=false; shdr_bc.Fluctuate=fluctuate; break;
            case "vram": shdr_vram.enabled=false; shdr_vram.Fluctuate=fluctuate; break;
            case "crt": shdr_crt.enabled=false; shdr_crt.Fluctuate=fluctuate; break;
            case "unsync": shdr_unsync.enabled=false; break;
            case "mos": shdr_mos.enabled=false; shdr_mos.Fluctuate=fluctuate; break;
            case "pix": shdr_pix.enabled=false; break;
            case "invc": shdr_invc.enabled=false; shdr_invc.Fluctuate=fluctuate; break;
            case "tint": shdr_tint.enabled=false; shdr_tint.Fluctuate=fluctuate; break;
            case "lim": shdr_lim.enabled=false; break;
            case "pos": shdr_pos.enabled=false; break;
            case "glitchMap": CarreraManager.Instance?.EnableGlitchTilemap(false); break;
            default: break;
        }
    }

    public void Clear(){
            bool fluctuate=false;
            shdr_bc.enabled=false; shdr_bc.Fluctuate=fluctuate;
            shdr_vram.enabled=false; shdr_vram.Fluctuate=fluctuate;
            shdr_crt.enabled=false; shdr_crt.Fluctuate=fluctuate;
            shdr_unsync.enabled=false;
            shdr_mos.enabled=false; shdr_mos.Fluctuate=fluctuate;
            shdr_pix.enabled=false;
            shdr_invc.enabled=false; shdr_invc.Fluctuate=fluctuate;
            shdr_tint.enabled=false; shdr_tint.Fluctuate=fluctuate;
            shdr_lim.enabled=false;
            shdr_pos.enabled=false;
    }
    public void ActivateEffect(string nameFX, bool fluctuate, bool activate){
        switch(nameFX){
            case "bc": shdr_bc.enabled=activate; shdr_bc.Fluctuate=fluctuate; break;
            case "vram": shdr_vram.enabled=activate; shdr_vram.Fluctuate=fluctuate; break;
            case "crt": shdr_crt.enabled=activate; shdr_crt.Fluctuate=fluctuate; break;
            case "unsync": shdr_unsync.enabled=activate; break;
            case "mos": shdr_mos.enabled=activate; shdr_mos.Fluctuate=fluctuate; break;
            case "pix": shdr_pix.enabled=activate; break;
            case "invc": shdr_invc.enabled=activate; shdr_invc.Fluctuate=fluctuate; break;
            case "tint": shdr_tint.enabled=activate; shdr_tint.Fluctuate=fluctuate; break;
            case "lim": shdr_lim.enabled=activate; break;
            case "pos": shdr_pos.enabled=activate; break;
            case "glitchMap": CarreraManager.Instance?.EnableGlitchTilemap(true); break;
            default: break;
        }
    }


}
