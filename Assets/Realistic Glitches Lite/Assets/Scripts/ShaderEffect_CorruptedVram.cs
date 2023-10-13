using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_CorruptedVram : MonoBehaviour {

	public float shift = 10;
	private Texture texture;
	private Material material;

	[Header("Fluctuations")]
	[HideInInspector] public bool Fluctuate=false;
	[SerializeField] float fluctuationSpeed;

	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/Distortion") );
		texture = Resources.Load<Texture>("Checkerboard-big");
	}
		
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if(Fluctuate)
			FluctuateVariable();
			
		material.SetFloat("_ValueX", shift);
		material.SetTexture("_Texture", texture);
		Graphics.Blit (source, destination, material);
		
	}

	float currentValue;
	float targetValue;
	void FluctuateVariable(){
		currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * fluctuationSpeed);
		shift=currentValue;
        if (Mathf.Approximately(currentValue, targetValue))
        {
                targetValue = Random.Range(3f, 30f);
        }
	}
}
