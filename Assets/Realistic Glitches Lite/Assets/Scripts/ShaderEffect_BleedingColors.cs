using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_BleedingColors : MonoBehaviour {

	public float intensity = 30;
	public float shift = 0.5f;

	[Header("Fluctuations")]
	[HideInInspector] public bool Fluctuate=false;
	[SerializeField] float fluctuationSpeed;
	private Material material;

	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/BleedingColors") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if(Fluctuate)
		FluctuateVariable();
		
		material.SetFloat("_Intensity", intensity);
		material.SetFloat("_ValueX", shift);
		Graphics.Blit (source, destination, material);
	}

	void OnEnable(){
		intensity=30;
		targetValue=Random.Range(3f,30f);
		currentValue=18f;
	}

	float currentValue;
	float targetValue;
	void FluctuateVariable(){
		currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * fluctuationSpeed);
		intensity=currentValue;
        if (Mathf.Approximately(currentValue, targetValue))
        {
                targetValue = Random.Range(3f, 30f);
        }
	}
}
