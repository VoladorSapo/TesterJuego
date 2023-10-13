Shader "Custom/InvertColors"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NumRects("Number of Rectangles", int)=0
        _RectArray("Rect Array",Vector)=(0,0,0.1,0.1)
        _Rect1("Rect1",Vector)=(0,0,0,0)
        _Rect2("Rect2",Vector)=(0,0,0,0)
        _Rect3("Rect3",Vector)=(0,0,0,0)
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

            sampler2D _MainTex;
            int _NumRects;
            float4 _RectArray[20];
            float4 _Rect1;
            float4 _Rect2;
            float4 _Rect3;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

           
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			

			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col = tex2D(_MainTex, i.uv);
                    float4 rect = _Rect1;
                    if (i.uv.x >= rect.x && i.uv.x <= (rect.x + rect.z) &&
                        i.uv.y >= rect.y && i.uv.y <= (rect.y + rect.w))
                    {
                        col.rgb=1.0-col.rgb;
                        return col; // Red color
                    }
                    rect=_Rect2;
                    if (i.uv.x >= rect.x && i.uv.x <= (rect.x + rect.z) &&
                        i.uv.y >= rect.y && i.uv.y <= (rect.y + rect.w))
                    {
                        col.rgb=1.0-col.rgb;
                        return col; // Red color
                    }
                    rect=_Rect3;
                    if (i.uv.x >= rect.x && i.uv.x <= (rect.x + rect.z) &&
                        i.uv.y >= rect.y && i.uv.y <= (rect.y + rect.w))
                    {
                        col.rgb=1.0-col.rgb;
                        return col; // Red color
                    }
                
                
                return col;

            }
			ENDCG
        }
        
    }
    FallBack "Diffuse"
}
