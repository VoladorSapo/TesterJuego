Shader "Custom/Mosaic"
{
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NumTilesX("Number of X Tiles", Range(1,1000))=10
        _NumTilesY("Number of Y Tiles", Range(1,1000))=10
	}

	SubShader
	{
		Tags{"Queue"="Transparent"}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};
			
			sampler2D _MainTex;
			float _NumTilesX;
            float _NumTilesY;
            
			//Vertex Shader 
			v2f vert (appdata_t v){
			   v2f o;
			   o.vertex = UnityObjectToClipPos (v.vertex);
			   o.uv.x =v.uv.x*_NumTilesX; //Escala UV para controlar el tamaño de las tiles
               o.uv.y=v.uv.y*_NumTilesY;
			   return o; 
			}

			// Fragment shader
			fixed4 frag (v2f i) : SV_Target
			{
                //Aplica el efecto de mosaico
				float2 mosaicUV; 
                mosaicUV.x=floor(i.uv.x)/_NumTilesX;
                mosaicUV.y=floor(i.uv.y)/_NumTilesY;

				fixed4 col = tex2D(_MainTex,mosaicUV);
				return col;
			}
			ENDCG
		}
	}
}
