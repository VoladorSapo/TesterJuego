Shader "Custom/AlterSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _moveX("Offset X", float)=0
        _moveY("Offset Y", float)=0
        _ValueX("Corrupcion Sprite X", float)=0
        _ValueY("Corrupcion Sprite Y", float)=0
    }

    
    SubShader
    {
        Tags {"Queue"="Transparent"}

        Pass
        {
            ZTest Off
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform float _moveX, _moveY, _ValueX, _ValueY;
            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                
                o.uv = v.uv+float2(_moveX,_moveY);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half2 rnd = _ValueX + i.uv.x;
                half2 rnd2 = _ValueY + i.uv.y;
				half2 n = tex2D(_MainTex, i.uv);
				half2 ogUV=i.uv.xy;

				if(fmod(ogUV.y,0.1f)>=0.05f)
				i.uv.x += n * 0.01f * rnd;
				else
				i.uv.x -= n * 0.01f * rnd;

                if(fmod(ogUV.x,0.1f)>=0.05f)
				i.uv.y += n * 0.01f * rnd2;
				else
				i.uv.y -= n * 0.01f * rnd2;

                fixed4 originalColor = tex2D(_MainTex, i.uv.xy);
                return originalColor;
            }
            ENDCG
        }
    }
}