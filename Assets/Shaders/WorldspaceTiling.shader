Shader "Custom/WorldspaceTilling" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
    _SecondTex("NotBase", 2D) = "white" {}
}
SubShader {
	Tags { "RenderType"="Opaque" }

CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _SecondTex;

struct Input
{
	float3 worldNormal;
	float3 worldPos;
};

void surf (Input IN, inout SurfaceOutput o)
{
	float2 UV;
	fixed4 c;

	if(abs(IN.worldNormal.x) > 0.5)
	{
		UV = IN.worldPos.yz;
		
		float Pi = acos(-1.0);
		float theta=90*Pi/180;
		float4 row0=float4(cos(theta),-sin(theta),0,0);
		float4 row1=float4(sin(theta),cos(theta),0,0);
		float4 row2=float4(0,0,1,0);
		float4 row3=float4(0,0,0,1);
		float4x4 rotator=float4x4(row0,row1,row2,row3);

		c = tex2D(_MainTex, mul(rotator, float4(UV, 0, 1.0)).xy);
	}
	else if(abs(IN.worldNormal.z) > 0.5)
	{
		UV = IN.worldPos.xy;
		c = tex2D(_MainTex, UV);
	}
	else
	{
		UV = IN.worldPos.xz;
		c = tex2D(_MainTex, UV);
	}

	o.Albedo = c.rgb;
}
ENDCG
}
}