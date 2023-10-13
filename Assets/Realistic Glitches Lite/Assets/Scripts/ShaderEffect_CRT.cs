using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_CRT : MonoBehaviour {

	public float scanlineIntensity = 100;
	public int scanlineWidth = 1;
	public float YShift;

	[Header("Fluctuations")]
	[HideInInspector] public bool Fluctuate=false;
	[SerializeField] float fluctuationSpeed;
//	public Color scanlineColor = Color.black;
//	public bool tVBulge = true;
	private Material material_Displacement;
	private Material material_Scanlines;

	void Awake ()
	{
		material_Scanlines = new Material( Shader.Find("Hidden/Scanlines") );
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if(Fluctuate)
			FluctuateVariable();

		material_Scanlines.SetFloat("_YShift",YShift);
		material_Scanlines.SetFloat("_Intensity", scanlineIntensity * 0.01f);
		material_Scanlines.SetFloat("_ValueX", scanlineWidth);

		Graphics.Blit (source, destination, material_Scanlines);
	}

	float currentValue;
	float targetValue;
	void FluctuateVariable(){
		currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * fluctuationSpeed);
		YShift=currentValue;
        if (Mathf.Approximately(currentValue, targetValue))
        {
                targetValue = Random.Range(3f, 30f);
        }
	}
}
