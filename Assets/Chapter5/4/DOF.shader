Shader "Custom/DOF" {  
  
    Properties{  
        _MainTex("Base (RGB)", 2D) = "white" {}  
        _BlurTex("Blur", 2D) = "white"{}  
    }  
  
    SubShader  
    {  
        Pass  
        {  

            Cull Off  
            ZTest Off  
            ZWrite Off  
            Fog{ Mode Off }  
            ColorMask RGBA  
  
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
                float4 pos : SV_POSITION;  
                float2 uv  : TEXCOORD0;   
            };  
          
            sampler2D _MainTex;  
			float4 _MainTex_ST;
            sampler2D _BlurTex;  
            sampler2D_float _CameraDepthTexture;  
            float _focalDistance;  
            float _scaleParam;  
          
            v2f vert(appdata v)  
            {  
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }  
          
            fixed4 frag(v2f i) : SV_Target  
            {  
                fixed4 ori = tex2D(_MainTex, i.uv);  
                fixed4 blur = tex2D(_BlurTex, i.uv);  

                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);  
                depth = Linear01Depth(depth);  
                  
                fixed4 final = (depth <= _focalDistance) ? ori : lerp(ori, blur, clamp((depth - _focalDistance) * _scaleParam, 0, 1));  
          
                return final;  
            }  
            ENDCG  
        }  
  
    }  
} 