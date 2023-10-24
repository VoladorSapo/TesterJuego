using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect_Pixelation : MonoBehaviour
{
	[Min(0.1f)]
    [SerializeField] private Vector2 pixelationFactor;
    private Material material;

    // Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Custom/Pixelation") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
        material.SetFloat("_CellSizeX",pixelationFactor.x);
        material.SetFloat("_CellSizeY",pixelationFactor.y);
        material.SetFloat("_ScreenWidth",Screen.width);
        material.SetFloat("_ScreenHeight",Screen.height);
		Graphics.Blit (source, destination, material);
	}
}
