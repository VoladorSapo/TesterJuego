﻿Shader "Hidden/Distortion"
{
	Properties
	{
		_Intensity ("Displacement value", Range(0,1)) = 0.01
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Texture ("Displacement map (RGB)", 2D) = "black" {}
		_ValueX("Noise value", Range(0,10)) = 4.51
	}
	SubShader
	{
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			half _ValueX;

			struct v2f {
			   float4 pos : POSITION;
			   half2 uv : TEXCOORD0;
			};
			   
			//Our Vertex Shader 
			v2f vert (appdata_img v){
			   v2f o;
			   o.pos = UnityObjectToClipPos (v.vertex);
			   o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
			   return o; 
			}

			    
			uniform sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders
			uniform sampler2D _Texture;

			fixed4 frag(v2f i) : COLOR {

				half2 rnd = _ValueX + i.uv.x;
				half2 n = tex2D(_Texture, i.uv);
				
				if(fmod(i.uv.y,0.1f)>=0.05f)
				i.uv.x += n * 0.01f * rnd;
				else
				i.uv.x -= n * 0.01f * rnd;
				//i.uv.y += n * 0.01f;

				float4 c = tex2D(_MainTex, i.uv);
				
		       	return c;

         	}
			ENDCG
		}
	}
}
