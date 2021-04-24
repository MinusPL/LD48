// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UGP/ToonDefault"
{
    Properties
    {
		[Header(Base Color)]
		[HDR]_Color("Color", Color) = (0.8,0.8,0.8,1)
		_MainTex("Main Texture", 2D) = "white" {}
		// Ambient light is applied uniformly to all surfaces on the object.
		[Header(Ambient Color)]
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		[Header(Specular)]
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		// Controls the size of the specular reflection.
		_Glossiness("Glossiness", Float) = 32
		[Header(Rim)]
		[HDR]
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		// Control how smoothly the rim blends when approaching unlit
		// parts of the surface.
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
		[Header(Outline)]
		_OutlineWidth("Outline Width", Range(0, 10)) = 1 // Thickness of outline
		[HDR]_OutlineColor("Outline Color", Color) = (0,0,0,1)

		[Header(Other Stuff)]
		[Toggle(_)] _Inverse_Clipping("Inverse_Clipping", Float) = 0
		_Clipping_Level("Clipping_Level", Range(0, 1)) = 0
	}

    SubShader
    {
        Pass
		{
			Tags { "RenderType" = "Opaque" }

			Cull Off

			Stencil
			{
				Ref 1
				Comp Always
				Pass Replace
			}

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			// Compile multiple versions of this shader depending on lighting settings.
			#pragma multi_compile_fwdbase
				#pragma multi_compile_shadowcaster
	#pragma multi_compile_instancing 
			//#pragma shader_feature_local_fragment _UseAlphaClipping

			#include "UnityCG.cginc"
			// Files below include macros and functions to assist
			// with lighting and shadows.
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				// Macro found in Autolight.cginc. Declares a vector4
				// into the TEXCOORD2 semantic with varying precision 
				// depending on platform target.
				SHADOW_COORDS(2)
			};

			//Define properites used in vertex shader
			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				// Defined in Autolight.cginc. Assigns the above shadow coordinate
				// by transforming the vertex from world space to shadow-map space.
				TRANSFER_SHADOW(o)
				return o;
			}

			float4 _Color;

			float4 _AmbientColor;

			float4 _SpecularColor;
			float _Glossiness;

			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;

			float _Inverse_Clipping;
			float _Clipping_Level;

			float4 frag(v2f i) : SV_TARGET
			{
				float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);

				// Lighting below is calculated using Blinn-Phong,
				// with values thresholded to creat the "toon" look.
				// https://en.wikipedia.org/wiki/Blinn-Phong_shading_model

				// Calculate illumination from directional light.
				// _WorldSpaceLightPos0 is a vector pointing the OPPOSITE
				// direction of the main directional light.
				float NdotL = dot(_WorldSpaceLightPos0, normal);

				// Samples the shadow map, returning a value in the 0...1 range,
				// where 0 is in the shadow, and 1 is not.
				float shadow = SHADOW_ATTENUATION(i);
				// Partition the intensity into light and dark, smoothly interpolated
				// between the two to avoid a jagged break.
				float val = NdotL * shadow;
				float lightIntensity = smoothstep(0, 0.04, val);
				// Multiply by the main directional light's intensity and color.
				float4 light = lightIntensity * _LightColor0;

				// Calculate specular reflection.
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				// Multiply _Glossiness by itself to allow artist to use smaller
				// glossiness values in the inspector.
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;

				// Calculate rim lighting.
				float rimDot = 1 - dot(viewDir, normal);
				// We only want rim to appear on the lit side of the surface,
				// so multiply it by NdotL, raised to a power to smoothly blend it.
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				float4 sample = tex2D(_MainTex, i.uv);

				//float Set_MainTexAlpha = sample.a;
				//float _IsBaseMapAlphaAsClippingMask_var = lerp(_ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask);
				//float _Inverse_Clipping_var = lerp(Set_MainTexAlpha, (1.0 - Set_MainTexAlpha), _Inverse_Clipping);
				//float Set_Clipping = saturate((_Inverse_Clipping_var + _Clipping_Level));

				//clip(Set_Clipping - 0.5);

				//float tLightIntensity = NdotL > 0 ? 1 : 0;
				//float tLight = tLightIntensity * _LightColor0;


				return (light + (_AmbientColor * (1.0 - light)) + (specular * rim)) * _Color * sample;
				//return light;
				//return _Color * sample * (tLight + (_AmbientColor * (1.0 - tLight)));
			}

			ENDCG
		}

		Pass
		{
			Name "Outline"

			Cull Off

			Stencil
			{
				Ref 1
				Comp Greater
			}

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
				float4 color : TEXCOORD1;
			};


			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 color : TEXCOORD0;
			};

			float _OutlineWidth;
			sampler2D _MainTex;
			float4 _OutlineColor;

			v2f vert(appdata v)
			{
				v2f o;
				float4 clipPosition = UnityObjectToClipPos(v.pos);
				float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, v.normal));

				float2 offset = normalize(clipNormal.xy) * _OutlineWidth * clipPosition.w;


				float aspect = _ScreenParams.x / _ScreenParams.y;
				offset.y *= aspect;

				// Applying offset
				clipPosition.xy += offset;

				o.pos = clipPosition;

				o.color = tex2Dlod(_MainTex, float4(v.texCoord.xy, 0, 0)) * _OutlineColor;

				return o;
				/*v2f o;
				v.pos.xyz *= _OutlineWidth;
				v.normal *= -1;
				o.pos = UnityObjectToClipPos(v.pos);
				return o;*/
			}

			float4 frag(v2f i) : SV_TARGET
			{
				return i.color;
			}

			ENDCG
		}
		
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
    FallBack "Diffuse"
}
