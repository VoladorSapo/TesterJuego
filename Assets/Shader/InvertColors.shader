Shader "Custom/InvertColors"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NumRects("Number of Rectangles", Int)=1
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM

			#pragma exclude_renderers flash
	  		#pragma vertex vert_img
	  		#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
	  		#include "UnityCG.cginc"

            sampler2D _MainTex;
            uniform int _NumRects;
            uniform float4 _RectArray[256];

			

			fixed4 frag (v2f_img i) : SV_Target
			{
                fixed4 col = tex2D(_MainTex, i.uv);

                    for(int j=0; j<_NumRects; j++){
                        float4 rect=_RectArray[j];

                        if (i.uv.x >= rect.x && i.uv.x <= (rect.x + rect.z) &&
                        i.uv.y >= rect.y && i.uv.y <= (rect.y + rect.w))
                        {
                        col.rgb=1.0-col.rgb;
                        return col;
                        }
                    }
                
                
                return col;

            }
			ENDCG
        }
        
    }
    FallBack "Diffuse"
}
