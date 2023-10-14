Shader "Custom/PixelPerfection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            uniform float _PixelSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float2 pixelSizeUV=float2(_ScreenParams.x/_PixelSize,_ScreenParams.y/_PixelSize);
                float pixelX=pixelSizeUV.x;
                float pixelY=pixelSizeUV.y;

                o.uv = float2(floor(pixelX * v.uv.x) / pixelX, floor(pixelY * v.uv.y) / pixelY);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture at the original UV coordinates
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
        
    }
}
