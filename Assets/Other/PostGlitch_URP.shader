Shader "Hidden/Custom/PostGlitch2"
{
    Properties
    {
        _GlitchTex("GlitchTex", 2D) = "white" {}
        _NoiseStrength("Noise", Range(0,1)) = 0.5
        _GlitchStrength("Glitch", Range(0,1)) = 0.5
        _Scale("Scale", Range(0,2)) = 1
        _TimeScale("TimeScale", Range(0,5)) = 1
        _GlitchColor("GlitchColor", Color) = (1.0, 0.0, 0.0, 1.0)
        _ColorSplitX("ColorSplitX", Range(0,1)) = 0.5
        _ColorSplitY("ColorSplitY", Range(0,1)) = 0.2
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "IgnoreProjector" = "True"
        }

        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

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

            TEXTURE2D(_CameraColorTexture);
            SAMPLER(sampler_CameraColorTexture);
            float4 _CameraColorTexture_ST;

            float4 _GlitchColor;
            TEXTURE2D(_GlitchTex);
            SAMPLER(sampler_GlitchTex);

            float _Scale, _TimeScale, _GlitchStrength, _NoiseStrength, _ColorSplitX, _ColorSplitY;

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                // Positionen berechnen
                VertexPositionInputs vpi = GetVertexPositionInputs(v.vertex.xyz);
                // Speichern
                o.vertex = vpi.positionCS;
                o.uv = TRANSFORM_TEX(v.uv, _CameraColorTexture);
                // An Fragment weitergeben
                return o;
            }

            float nrand(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            float4 frag (v2f i) : SV_Target
            {
                return SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, i.uv);

                float2 uv = i.uv * 5;

                float glitch = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.y * 0.1152 * _Scale + int(_Time.z * 5.21 * _TimeScale) * 0.28, 0.5)).x;
                float glitch3 = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.y * 0.2123 * _Scale + int(_Time.z * 8.21 * _TimeScale) * 0.28, 0.5)).x;
                float glitch2 = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.y * 0.0113 * _Scale + int(_Time.z * 3.22 * _TimeScale) * 0.41, 0.5)).x * -0.5;
                float glitch4 = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.x * 0.1421 * _Scale + int(_Time.z * 1.21 * _TimeScale) * 0.18, 0.5)).x;

                float2 offset = float2(0.0, 0.0);
                offset.x += (glitch - glitch3 * 0.5) * (0.4 + (nrand(uv + _Time.yy) * 2 - 1) * 0.5 * _NoiseStrength) * _GlitchStrength; // Noise
                offset.x += (nrand(uv + 2124.1 + _Time.yy) * 2 - 1) * 0.1 * _NoiseStrength * _GlitchStrength;                           // Side glitch
                offset.y += glitch2 * 0.5 * _GlitchStrength;                                                                            // Compress/Stretch Glitch (outcomment to disable movement up-down)
                offset.xy += (float2(0.5, 0.5) - i.uv) * min(glitch3, glitch4) * 0.5 * _GlitchStrength;                                 // Rectangle Artefacts

                float4 s1 = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, i.uv + offset * float2(1 - _ColorSplitX, 1 - _ColorSplitY));
                float4 s2 = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, i.uv + offset); // Color split
                float4 col = float4(lerp(_GlitchColor.rgb, s1.rgb * _GlitchColor.rgb, s1.a) + lerp(1.0 - _GlitchColor.rgb, (1.0 - _GlitchColor.rgb) * s2.rgb, s2.a), (s1.a + s2.a) / 2.0);
                col.rgb = lerp(col.rgb, _GlitchColor.rgb, _GlitchStrength * step(0.6, min(glitch3, glitch4))); // Color rectangle Artefacts

                return col;
            }
            ENDHLSL
        }
    }
    // Error
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
