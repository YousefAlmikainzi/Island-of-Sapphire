Shader "Unlit/BossShader"
{
    Properties
    {
        _FresnelPower ("Fresnel Power", Float) = 1
        _GlowIntensity ("Glow Intensity", Float) = 2
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off
        
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD1;
            };

            float _FresnelPower;
            float _GlowIntensity;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex = v.vertex + sin(_Time * .2);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                i.normal = normalize(i.normal);
                float3 viewDir = _WorldSpaceCameraPos - i.worldPos;
                float fresnel = pow(1.0 - saturate(dot(normalize(viewDir), i.normal)), _FresnelPower);

                float glow = abs(pow(1 - fresnel, 10));
                clip(glow - 0.5); 

                float3 emissionColor = _Color.rgb * glow * _GlowIntensity;

                return float4(emissionColor, 1);
            }
            ENDCG
        }
    }
}
