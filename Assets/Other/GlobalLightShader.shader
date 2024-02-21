Shader "Hidden/GlobalLightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightTex ("LightTexture", 2D) = "white" {}
        _ForceTex ("ForceTexture", 2D) = "white" {}
        _Add("Add", Float) = 1
        _Force("Force", Float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            sampler2D _LightTex;
            sampler2D _ForceTex;
            Float _Add;
            Float _Force;
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 force = tex2D(_ForceTex, i.uv);
                
                i.uv += _Force * (force.xy - fixed2(0.5019608, 0.5019608));
                
                fixed4 col = tex2D(_MainTex, i.uv);
                col = col * tex2D(_LightTex, i.uv) * _Add;

                return col;
            }
            ENDCG
        }
    }
}
