Shader "Custom/GlitchSprite"
{
	Properties
	{
		[HDR]
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Texture", 2D) = "white" {}
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
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" "IgnoreProjector"="True"}
		Blend srcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 screenspace : TEXCOORD1;
				float4 color : COLOR;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			
			float4 _GlitchColor;
			sampler2D _GlitchTex, _NoiseTex;
			float _Scale, _TimeScale, _GlitchStrength, _NoiseStrength, _ColorSplitX, _ColorSplitY;

			v2f vert (appdata v)
			{
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v);

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.screenspace = o.vertex / o.vertex.w * float4(_ScreenParams.y / _ScreenParams.x, 1, 1, 1);
				o.color = v.color * _Color;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			float nrand(float2 uv)
			{
				return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.screenspace * 5;

				float glitch  = tex2D(_GlitchTex, float2(uv.y * 0.1152 * _Scale + int(_Time.z * 5.21 * _TimeScale) * 0.28, 0.5)).x;
				float glitch3 = tex2D(_GlitchTex, float2(uv.y * 0.2123 * _Scale + int(_Time.z * 8.21 * _TimeScale) * 0.28, 0.5)).x;
				float glitch2 = tex2D(_GlitchTex, float2(uv.y * 0.0113 * _Scale + int(_Time.z * 3.22 * _TimeScale) * 0.41, 0.5)).x * -0.5;
				float glitch4 = tex2D(_GlitchTex, float2(uv.x * 0.1421 * _Scale + int(_Time.z * 1.21 * _TimeScale) * 0.18, 0.5)).x;

				float2 offset = float2(0.0, 0.0);
				offset.x += (glitch - glitch3 * 0.5) * (0.4 + (nrand(uv + _Time.yy) * 2 - 1) * 0.5 * _NoiseStrength) * _GlitchStrength; // Noise
				offset.x += (nrand(uv + 2124.1 + _Time.yy) * 2 - 1) * 0.1 * _NoiseStrength * _GlitchStrength;                           // Side glitch
				offset.y += glitch2 * 0.5 * _GlitchStrength;                                                                            // Compress/Stretch Glitch (outcomment to disable movement up-down)
				offset.xy += (float2(0.5, 0.5) - i.uv) * min(glitch3, glitch4) * 0.5 * _GlitchStrength;                                 // Rectangle Artefacts

				fixed4 s1 = tex2D(_MainTex, i.uv + offset * float2(1 - _ColorSplitX, 1 - _ColorSplitY)), s2 = tex2D(_MainTex, i.uv + offset); // Color split
				fixed4 col = i.color * fixed4(lerp(_GlitchColor.rgb, s1.rgb * _GlitchColor.rgb, s1.a) + lerp(1.0 - _GlitchColor.rgb, (1.0 - _GlitchColor.rgb) * s2.rgb, s2.a), (s1.a + s2.a) / 2.0);
				col.rgb = lerp(col.rgb, _GlitchColor, _GlitchStrength * step(0.6, min(glitch3, glitch4))); // Color rectangle Artefacts
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
