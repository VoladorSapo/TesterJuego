using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderEffect_PixelPerfection : MonoBehaviour
{
        [SerializeField] private float _pixelSize;
        private Material m_material;
		private Shader shader;

		private Material material {
			get {
				if (m_material == null) {
					shader = Shader.Find("Custom/PixelPerfection");
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
		}

        public void OnRenderImage(RenderTexture src, RenderTexture dest){
            material.SetFloat("_PixelSize",_pixelSize);
            Graphics.Blit(src,dest,material);
        }
}
