Shader "Hidden/Custom/PostGlitch"
{
	HLSLINCLUDE

#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	TEXTURE2D_SAMPLER2D(_GlitchTex, sampler_GlitchTex);
	float4 _GlitchColor;
	float _Scale, _TimeScale, _GlitchStrength, _NoiseStrength, _ColorSplitX, _ColorSplitY;

	float nrand(float2 uv)
	{
		return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
	}

	float4 Frag(VaryingsDefault i) : SV_Target
	{
		//float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

		float2 uv = i.texcoord * 5;

		float glitch  = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.y * 0.1152 * _Scale + int(_Time.z * 5.21 * _TimeScale) * 0.28, 0.5)).x;
		float glitch3 = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.y * 0.2123 * _Scale + int(_Time.z * 8.21 * _TimeScale) * 0.28, 0.5)).x;
		float glitch2 = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.y * 0.0113 * _Scale + int(_Time.z * 3.22 * _TimeScale) * 0.41, 0.5)).x * -0.5;
		float glitch4 = SAMPLE_TEXTURE2D(_GlitchTex, sampler_GlitchTex, float2(uv.x * 0.1421 * _Scale + int(_Time.z * 1.21 * _TimeScale) * 0.18, 0.5)).x;

		float2 offset = float2(0.0, 0.0);
		offset.x += (glitch - glitch3 * 0.5) * (0.4 + (nrand(uv + _Time.yy) * 2 - 1) * 0.5 * _NoiseStrength) * _GlitchStrength; // Noise
		offset.x += (nrand(uv + 2124.1 + _Time.yy) * 2 - 1) * 0.1 * _NoiseStrength * _GlitchStrength;                           // Side glitch
		offset.y += glitch2 * 0.5 * _GlitchStrength;                                                                            // Compress/Stretch Glitch (outcomment to disable movement up-down)
		offset.xy += (float2(0.5, 0.5) - i.texcoord) * min(glitch3, glitch4) * 0.5 * _GlitchStrength;                                 // Rectangle Artefacts

		float4 s1 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + offset * float2(1 - _ColorSplitX, 1 - _ColorSplitY));
		float4 s2 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + offset); // Color split
		float4 col = float4(lerp(_GlitchColor.rgb, s1.rgb * _GlitchColor.rgb, s1.a) + lerp(1.0 - _GlitchColor.rgb, (1.0 - _GlitchColor.rgb) * s2.rgb, s2.a), (s1.a + s2.a) / 2.0);
		col.rgb = lerp(col.rgb, _GlitchColor.rgb, _GlitchStrength * step(0.6, min(glitch3, glitch4))); // Color rectangle Artefacts

		return col;
	}

	ENDHLSL

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

			Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
		}
	}
}