Shader "Unlit/NormalMapShader"
{
Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset]_NormalMap ("Normal Map", 2D) = "bump" {}
        _BumpScale ("Bump Scale", Float) = 1
        _Color ("Color", Color) = (1,1,1,1)
        _Smoothness ("Smoothness", Range(0, 1)) = .5
        [Gamma]_Metallic ("Metallic", Range(0, 1)) = 0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityPBSLighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normals : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float3 tangent : TEXCOORD3;
                float3 bitangent : TEXCOORD4;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NormalMap;
            float4 _Color;
            float _Smoothness;
            float _Metallic;
            float _BumpScale;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normals = UnityObjectToWorldNormal(v.normals);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.tangent = UnityObjectToWorldDir(v.tangent.xyz);
                o.bitangent = cross(o.normals, o.tangent);
                o.bitangent = o.bitangent * v.tangent.w * unity_WorldTransformParams;
                return o;
            }

            UnityLight CreateLight(v2f i)
            {
                UnityLight light;
                light.color = _LightColor0;
                light.dir = normalize(_WorldSpaceLightPos0.xyz);
                light.ndotl = DotClamped(i.normals, light.dir);
                return light;
            }

            UnityIndirect CreateIndirect(v2f i)
            {
                UnityIndirect Indirect;
                Indirect.diffuse = 0;
                Indirect.specular = 0;
                return Indirect;
            }
            
            void InitializeNormal(inout v2f i)
            {
                /*i.normals.xy = tex2D(_NormalMap, i.uv).wy * 2 - 1;
                i.normals.xy *= _BumpScale;
                i.normals.z = sqrt(1 - dot(i.normals.xy, i.normals.xy));*/
                i.normals = UnpackScaleNormal(tex2D(_NormalMap, i.uv), _BumpScale);
                i.normals = i.normals.xzy;
                i.normals = normalize(i.normals);
            }
            
            float4 frag (v2f i) : SV_Target
            {
                InitializeNormal(i);
                float3 tex = tex2D(_MainTex, i.uv);
                float3 albedo = _Color * tex;

                float oneMinusReflect;
                float3 metallicTint;

                albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, metallicTint, oneMinusReflect);

                float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPos);

                float4 final = UNITY_BRDF_PBS(albedo, metallicTint, oneMinusReflect, _Smoothness,
                    i.normals, viewDirection, CreateLight(i), CreateIndirect(i));
                return final;
            }
            ENDCG
        }
    }
}
