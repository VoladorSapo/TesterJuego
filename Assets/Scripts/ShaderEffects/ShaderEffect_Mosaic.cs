using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect_Mosaic : MonoBehaviour
{
    [SerializeField] private float _numberOfTilesX;
    [SerializeField] private float _numberOfTilesY;

	[HideInInspector] public bool Fluctuate=false;
	[Header("Fluctuate")]
	[SerializeField] float minX;
	[SerializeField] float minY, maxX, maxY;
	[SerializeField] float fluctuationSpeed;
    private Material material;

    // Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Custom/Mosaic") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if(Fluctuate)
			FluctuateVariable();

        material.SetFloat("_NumTilesX", _numberOfTilesX);
        material.SetFloat("_NumTilesY", _numberOfTilesY);
		Graphics.Blit (source, destination, material);
	}

	float currentValueX, currentValueY;
	float targetValueX, targetValueY;
	void FluctuateVariable(){

		currentValueX = Mathf.Lerp(currentValueX, targetValueX, Time.deltaTime * fluctuationSpeed);
		currentValueY = Mathf.Lerp(currentValueY, targetValueY, Time.deltaTime * fluctuationSpeed);

        if (Mathf.Approximately(currentValueX, targetValueX))
        {
                targetValueX = Random.Range(minX, maxX);
        }
		if (Mathf.Approximately(currentValueY, targetValueY))
        {
                targetValueX = Random.Range(minY, maxY);
        }
	}
}
