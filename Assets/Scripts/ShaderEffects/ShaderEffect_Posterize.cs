using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShaderEffect_Posterize : MonoBehaviour
{
    [Header("Posterizar Colores")]
    [SerializeField] private int Red;
    [SerializeField] private int Green;
    [SerializeField] private int Blue;


    private Material m_material;
    private Shader shader;
    private Material material {
			get {
				if (m_material == null) {
					shader = Shader.Find("Unlit/Posterize");
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
	}

    public void OnRenderImage(RenderTexture src, RenderTexture dest){
        material.SetInt("_Red",Red);
        material.SetInt("_Green",Green);
        material.SetInt("_Blue",Blue);
        Graphics.Blit(src,dest,material);
    }
}
