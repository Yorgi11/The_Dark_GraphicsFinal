Shader "Custom/Bloom"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _BloomColor ("Bloom Color", Color) = (1,1,1,1)
        _Intensity ("Bloom Intensity", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        struct Input
        {
            float2 uv_MainTex;
        };
        fixed4 _Color;
        fixed4 _BloomColor;
        float _Intensity;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color.rgb;
            o.Emission = _BloomColor.rgb * _Intensity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
