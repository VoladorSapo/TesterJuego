using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect_Mosaic : MonoBehaviour
{
    [SerializeField] private float _numberOfTilesX;
    [SerializeField] private float _numberOfTilesY;
    private Material material;

    // Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Custom/Mosaic") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
        material.SetFloat("_NumTilesX", _numberOfTilesX);
        material.SetFloat("_NumTilesY", _numberOfTilesY);
		Graphics.Blit (source, destination, material);
	}
}
