using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShaderEffect_InvertColorBits : MonoBehaviour
{
   [SerializeField] [Range(1f,20f)] private int numRectanglesTotal;
   [SerializeField] [Range(1f,20f)] private int setNumRectangles;
   [SerializeField] private Vector2 limitSize;

    private float changeTimer;

    [HideInInspector] public bool Fluctuate=false;
    [Header("Fluctuate")]
	[SerializeField] int minNumRect;
    [SerializeField] float minRateTime, maxRateTime;
	
    
    private Vector4[] rectProp;
    private Material m_material;
	private Shader shader;

	private Material material {
			get {
				if (m_material == null) {
					shader = Shader.Find("Custom/InvertColors");
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
	}

    void CreateRectangles(){
        rectProp=new Vector4[numRectanglesTotal];
        for(int i=0; i<setNumRectangles; i++){
            rectProp[i].x=Random.Range(0f,1f);
            rectProp[i].y=Random.Range(0f,1f);
            rectProp[i].z=Random.Range(0f,limitSize.x);
            rectProp[i].w=Random.Range(0f,limitSize.y);
        }
    }

    void Update(){

        if(changeTimer<=0){
            if(Fluctuate)
            FluctuateVariable(out changeTimer, out num);
            else{
            changeTimer=minRateTime;
            num=minNumRect;
            }

            CreateRectangles();
        }else{
            changeTimer-=Time.deltaTime;
            
        }
    }
	
    int num;
    public void OnRenderImage(RenderTexture src, RenderTexture dest) {
			if (material && rectProp.Length > 0) {
                
				material.SetVectorArray("_RectArray", rectProp);
                material.SetInt("_NumRects",num);
				Graphics.Blit(src, dest, material);
			} else {
				Graphics.Blit(src, dest);
			}
	}

    
    void FluctuateVariable(out float changeTimer, out int num){
        changeTimer=Random.Range(minRateTime,maxRateTime);
        num=Random.Range(minNumRect,numRectanglesTotal+1);
	}
}
