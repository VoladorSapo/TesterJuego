using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_Tint : MonoBehaviour {

	public float y = 1;
	public float u = 1;
	public float v = 1;
//	public bool swapUV = false;
	private Material material;

	[HideInInspector] public bool Fluctuate=false;
    [Header("Fluctuate")]
	[SerializeField] float fluctuationTime;
	float fluctuationTimer;
	[Min(1)]
	[SerializeField] float fy, fu, fv;
    

	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/Tint") );
	}

	void Update(){
		if(Fluctuate){
			if(fluctuationTimer<=0){
				fluctuationTimer=fluctuationTime;
				y=Random.Range(1f,fy);
				u=Random.Range(1f,fu);
				v=Random.Range(1f,fv);
			}else{
				fluctuationTimer-=Time.deltaTime;
			}
		}else{
			fluctuationTimer=0;
			y=1; u=1; v=1;
		}
	}
	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_ValueX", y);
		material.SetFloat("_ValueY", u);
		material.SetFloat("_ValueZ", v);

		Graphics.Blit (source, destination, material);
	}
}
