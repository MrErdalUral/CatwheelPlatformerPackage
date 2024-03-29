﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Wind"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_OutColor ("Outline Color", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_DistortionStrength ("DistortionStrength", Float) = 1
	}

	SubShader
	{

		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		LOD 100
		GrabPass { "_GrabTexture"}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD3;
				half2 uv : TEXCOORD0;
				half2 uv_center : TEXCOORD1; //the position of our object center on the grab-pass texture
				float4 obj_vertex : TEXCOORD2; //our fragment position in local object space
			};
			
			fixed4 _Color;
			fixed4 _OutColor;
			fixed4 _ReplaceWhite;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.uv = ComputeGrabScreenPos(OUT.vertex);
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
				OUT.obj_vertex = IN.vertex;
				OUT.uv_center = ComputeGrabScreenPos(UnityObjectToClipPos(float4(0, 0, 0, 1)));

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _GrabTexture;
			float4 _MainTex_TexelSize;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			
			half _DistortionStrength;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				float2 coord = IN.texcoord;
				c.rgb *= c.a;
				if(c.a > 0.1){
					fixed4 result = tex2D(_GrabTexture, IN.uv + normalize(IN.uv - IN.uv_center) * _DistortionStrength * c.a);
					return result;
				} 
				return c;

				//half dist = length(IN.obj_vertex.xy);
				//if(c.a > 0.1 && c.a < 0.9)
				//{
				//	half2 uv = IN.uv;
				//	uv.x = uv.x+c.a * _DistortionStrength * normalize(IN.uv - IN.uv_center);
				//	result = tex2D(_GrabTexture, uv); //our uv, but shifted outwards (in local space)
				//	result.a = 1;X
				//}
				//else
				//{
				//	return c;
				//}
				//
				//return result;
			}
		ENDCG
		}
	}
}