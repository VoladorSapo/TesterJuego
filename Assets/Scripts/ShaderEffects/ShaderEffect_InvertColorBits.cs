using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShaderEffect_InvertColorBits : MonoBehaviour
{
   [SerializeField] [Range(1f,20f)] private int numRectangles;
   [SerializeField] private Vector2 limitSize;

   [SerializeField] private float changeRateTime;
    private float changeTimer;
    
    private Vector4[] rectProp;
    private Material material;

    // Creates a private material used to the effect
	void Awake ()
	{
       
		material = new Material( Shader.Find("Custom/InvertColors") );
	}

    void CreateRectangles(){
        rectProp=new Vector4[numRectangles];
        for(int i=0; i<numRectangles; i++){
            rectProp[i].x=Random.Range(0f,1f);
            rectProp[i].y=Random.Range(0f,1f);
            rectProp[i].z=Random.Range(0f,limitSize.x);
            rectProp[i].w=Random.Range(0f,limitSize.y);

        }
    }

    void Update(){
        if(changeTimer<=0){
            changeTimer=changeRateTime;
            CreateRectangles();
        }else{
            changeTimer-=Time.deltaTime;
        }
    }
	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
        material.SetVector("_Rect1",rectProp[0]);
        material.SetVector("_Rect2",rectProp[1]);
        material.SetVector("_Rect3",rectProp[2]);
        material.SetInt("_NumRects",numRectangles);
		Graphics.Blit (source, destination, material);
	}
}
