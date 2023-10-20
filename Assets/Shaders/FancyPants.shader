Shader "Custom/FancyPants" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _SpecularColor("Specular Color", Color) = (1,1,1,1)
        _Shininess("Shininess", Range(1, 100)) = 20
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0.5
        _AmbientColor("Ambient Color", Color) = (1,1,1,1)
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}

            CGPROGRAM
            #pragma surface surf Standard

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _SpecularColor;
            float _Shininess;
            float _Smoothness;
            float _Metallic;
            fixed4 _AmbientColor;

            struct Input {
                float2 uv_MainTex;
                float3 worldPos;
                float3 worldNormal;
            };

            void surf(Input IN, inout SurfaceOutputStandard o) {
                fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;

                // ambient
                fixed4 ambient = _AmbientColor * tex;

                // diffuse
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float diff = max(0, dot(IN.worldNormal, lightDir));
                float3 diffuse = _LightColor0.rgb * diff;

                // specular
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - IN.worldPos);
                float3 halfwayDir = normalize(lightDir + viewDir);
                float3 specular = 0;
                if (_SpecularColor.r != 0 && _SpecularColor.g != 0 && _SpecularColor.b != 0)
                {
                    float spec = pow(max(0, dot(IN.worldNormal, halfwayDir)), _Shininess);
                    specular = _SpecularColor * _LightColor0.rgb * spec;
                }

                o.Albedo = tex.rgb * (ambient.rgb + diffuse.rgb) + specular.rgb;
                o.Alpha = tex.a;
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;
            }

            ENDCG
        }

            FallBack "Diffuse"
}
