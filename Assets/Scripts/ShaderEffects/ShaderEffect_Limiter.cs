using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect_Limiter : MonoBehaviour
{
    [Header("Colors")]
		public Color[] colors;

		private Material m_material;
		private Shader shader;

		private Material material {
			get {
				if (m_material == null) {
					shader = Shader.Find("Custom/Limiter");
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
		}

		public void OnRenderImage(RenderTexture src, RenderTexture dest) {
			if (material && colors.Length > 0) {
				material.SetInt("_ColorCount", colors.Length);
				material.SetColorArray("_Colors", colors);

				Graphics.Blit(src, dest, material);
			} else {
				Graphics.Blit(src, dest);
			}
		}
}
