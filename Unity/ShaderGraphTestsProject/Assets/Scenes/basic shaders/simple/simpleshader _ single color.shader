// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SingleColor"
{
	Properties
	{
		// we have removed support for texture tiling/offset,
		// so make them not be displayed in material inspector
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
	}
		SubShader
	{
		Pass
	{
		CGPROGRAM
		// use "vert" function as the vertex shader
#pragma vertex vert
		// use "frag" function as the pixel (fragment) shader
#pragma fragment frag

		// vertex shader inputs
		struct appdata
	{
		float4 vertex : POSITION; // vertex position
		float2 uv : TEXCOORD0; // texture coordinate
	};

	// vertex shader outputs ("vertex to fragment")
	struct v2f
	{
		float2 uv : TEXCOORD0; // texture coordinate
		float4 vertex : SV_POSITION; // clip space position
	};

	// vertex shader
	v2f vert(appdata v)
	{
		v2f o;
		// transform position to clip space
		// (multiply with model*view*projection matrix)
		o.vertex = UnityObjectToClipPos(v.vertex);
		// just pass the texture coordinate
		o.uv = v.uv;
		return o;
	}

	// color from the material
	fixed4 _Color;

	// pixel shader, no inputs needed
	fixed4 frag() : SV_Target
	{
		return _Color; // just return it
	}
	ENDCG
	}
	}
}