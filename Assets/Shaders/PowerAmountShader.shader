Shader "Unlit/PowerAmountShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PowerLevel ("Power Level", Range(0, 1)) = 1.0
		_Brightness ("Brightness", Range(0, 1)) = 0.5
		_BorderColor ("Border Color", Color) = (0, 0, 0, 1)
		_BorderWidthX ("Border Width X", Range(0, 0.2)) = 0.1
		_BorderWidthY ("Border Width Y", Range(0, 0.2)) = 0.1
		_RemainingPowerAmount("Remaining Power Amount", Range(0, 0.1)) = 0.01
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_instancing
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			UNITY_INSTANCING_CBUFFER_START(PowerLevelProp)
				UNITY_DEFINE_INSTANCED_PROP(float, _PowerLevel)
			UNITY_INSTANCING_CBUFFER_END

			uniform float _Brightness;
			uniform float4 _BorderColor;
			uniform float _BorderWidthX;
			uniform float _BorderWidthY;
			uniform float _RemainingPowerAmount;
			
			v2f vert (appdata v)
			{
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Draw the border.
				if (i.uv.x < _BorderWidthX || i.uv.x > (1 - _BorderWidthX) || i.uv.y < _BorderWidthY || i.uv.y > (1 - _BorderWidthY))
				{
					return fixed4(_BorderColor);
				}

				// Pull in the instanced power level.
				UNITY_SETUP_INSTANCE_ID(i);
				float instancedPowerLevel = UNITY_ACCESS_INSTANCED_PROP(_PowerLevel);

				// Determine which part to make transparent.
				if (instancedPowerLevel + _RemainingPowerAmount == 0 || i.uv.x > (instancedPowerLevel + _RemainingPowerAmount))
				{
					return fixed4(0, 0, 0, 0);
				}

				// Draw the colored part.
				fixed4 col = tex2D(_MainTex, fixed2(saturate(instancedPowerLevel), 0));
				col.rgb = col.rgb * _Brightness;
				return col;
			}
			ENDCG
		}
	}
}
