Shader "Hidden/VHSShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			sampler2D _NoiseTex;

			v2f vert (appdata v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;

				#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1 - o.uv.y;
				#endif

				return o;
			}

			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = tex2D(_MainTex, i.uv);
				if (floor((i.uv.y / _MainTex_TexelSize.y) % 4) < 2){
					col.rgb = col.rgb * 0.8;
				}
				float noise = tex2D(_NoiseTex, i.uv).r;
				float noise2 = tex2D(_NoiseTex, -i.uv + (_Time.yy % 10)).r;
				if (noise > noise2 - 0.1 && noise < noise2 + 0.1)
					col.rgb +=  abs(noise - noise2) * 0.5;
				return col;
			}
			ENDCG
		}
	}
}
