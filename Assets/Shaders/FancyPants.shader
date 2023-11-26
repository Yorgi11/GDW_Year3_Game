Shader "Custom/FancyPants" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _AmbientColor("Ambient Color", Color) = (1, 1, 1, 1)
        _DiffuseColor("Diffuse Color", Color) = (1, 1, 1, 1)
        _Shininess("Shininess", Range(0, 1)) = 0.5
        _SpecColor("Specular Color", Color) = (1, 1, 1, 1)
        _Specular("Specular", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0.5
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #include "UnityCG.cginc"

            struct Input {
                float2 uv_MainTex;
                float3 worldNormal;
                float3 worldPos;
            };

            sampler2D _MainTex;
            fixed4 _AmbientColor;
            fixed4 _DiffuseColor;
            float _Shininess;
            float _Specular;
            float _Metallic;
            float _Smoothness;

            void surf(Input IN, inout SurfaceOutputStandard o) {
                fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);

                // Ambient Lighting
                fixed3 ambient = _AmbientColor.rgb * texColor.rgb;

                // Diffuse Lighting
                fixed3 normal = normalize(IN.worldNormal);
                fixed3 lightDir = normalize(UnityWorldSpaceLightDir(IN.worldPos));
                fixed3 diffuse = _DiffuseColor.rgb * max(dot(normal, lightDir), 0) * texColor.rgb;

                // Specular Lighting
                float3 viewDir = normalize(UnityWorldSpaceViewDir(IN.worldPos));
                float3 halfDir = normalize(lightDir + viewDir);
                float spec = pow(max(dot(normal, halfDir), 0), _Shininess) * _Specular;
                fixed3 specular = _SpecColor.rgb * spec;

                o.Albedo = ambient + diffuse;
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;
                o.Alpha = texColor.a;
            }
            ENDCG
        }

            FallBack "Diffuse"
}
