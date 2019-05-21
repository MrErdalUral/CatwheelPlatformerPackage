// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OutlineSprite"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_OutColor ("Outline Color", Color) = (1,1,1,1)
		_ReplaceWhite("Replace White",Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
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
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;
			fixed4 _OutColor;
			fixed4 _ReplaceWhite;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

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
				if( c.r == 1 && c.g==1 && c.b == 1 && c.a == 1)
				{
				 	return _ReplaceWhite;
				}
				if(c.a == 0)
				{
					fixed4 c2 = SampleSpriteTexture (coord+float2(_MainTex_TexelSize.x,0)) * IN.color;
					if(c2.a > 0)
						{c = _OutColor;c.rgb*=_OutColor.a;}

					c2 = SampleSpriteTexture (coord+ float2(-_MainTex_TexelSize.x,0)) * IN.color;
					if(c2.a > 0)
						{c = _OutColor;c.rgb*=_OutColor.a;}

					c2 = SampleSpriteTexture (coord+ float2(0,_MainTex_TexelSize.y)) * IN.color;
					if(c2.a > 0)
						{c = _OutColor;c.rgb*=_OutColor.a;}

					c2 = SampleSpriteTexture (coord+ float2(0,-_MainTex_TexelSize.y)) * IN.color;
					if(c2.a > 0)
						{c = _OutColor;c.rgb*=_OutColor.a;}
				}
				return c;
			}
		ENDCG
		}
	}
}