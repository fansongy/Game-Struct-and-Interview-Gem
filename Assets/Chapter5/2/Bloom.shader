Shader "Custom/Bloom"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BlurRadius ("_BlurRadius",Range(1,20) ) = 5
		_BloomFactor ("_BloomFactor",Range(0,1))=0.5
		_ColorThreshold ("_ColorThreshold",Color) = (0.5,0.5,0.5,1)
        _TextureSizeX ("_TextureSizeX",Float) = 256
        _TextureSizeY ("_TextureSizeY",Float) = 256
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			 sampler2D _MainTex;
			 float4 _MainTex_ST;
			 int _BlurRadius;
			 float _TextureSizeX;
			 float _TextureSizeY;
			 float _BloomFactor;
			 fixed4 _ColorThreshold;

			 fixed4 BlurTexture( float2 uv,float blurRadius,float textureSizeX,float textureSizeY)
            {
                float pixelDisX = 1.0/textureSizeX;  //像素间距
                float pixelDisY = 1.0/textureSizeY;  //像素间距
                int count = blurRadius * 2 +1; //每行的像素数量
                count *= count;

                float4 tmpColor = float4(0,0,0,0);
                for( int x = -blurRadius ; x <= blurRadius ; x++ )
                {
                    for( int y = -blurRadius ; y <= blurRadius ; y++ )
                    {
                        float4 color = tex2D(_MainTex,uv + float2(x * pixelDisX,y * pixelDisY));
						color = saturate(color - _ColorThreshold);
                        tmpColor += color;
                    }
                }
                return tmpColor/count;
            }

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 orgColor = tex2D(_MainTex,i.uv);
				fixed4 blurColor = BlurTexture(i.uv,_BlurRadius,_TextureSizeX,_TextureSizeY);
				fixed4 final = orgColor+blurColor * _BloomFactor;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, final);
				return final;
			}

			ENDCG
		}
	}
}
