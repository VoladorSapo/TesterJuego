Shader "Custom/AlterColor"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorCount("Number of Colors", Int)= 8
        _moveX("Offset X", Float)=0
        _moveY("Offset Y", Float)=0
    }

    
    SubShader
    {
        Tags {"RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            
            AlphaTest Less [_Cutoff]
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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            uniform fixed4 _Colors[256];
            uniform fixed4 _NewColors[256];
            uniform int _ColorCount;
            uniform float _moveX;
            uniform float _moveY;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                //v.texcoord* float2(_TilingX, _TilingY) + float2(_moveX,_moveY);
                //v.uv.x=fmod(v.uv.x + _moveX, 1);
                //v.uv.y=fmod(v.uv.y + _moveY, 1);

                
                o.uv = v.uv+float2(_moveX,_moveY);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 originalColor = tex2D(_MainTex, i.uv.xy);

                for (int i = 0; i <_ColorCount; i++) {
                    if ((distance(originalColor.rgb, _Colors[i].rgb) < 0.05))
                    {
                        originalColor.rgb=_NewColors[i].rgb;
                        return originalColor;
                    }
                }
                
                return originalColor;
            }
            ENDCG
        }
    }
}