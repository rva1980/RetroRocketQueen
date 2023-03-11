// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/LCD"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}

        _RedCoef("Red Coef", Float) = 0.6
        _GreenCoef("Green Coef", Float) = 0.8
        _BlueCoef("Blue Coef", Float) = 0.2

        _Contrast("Contrast", Float) = -2
        _Brightness("Brightness", Float) = 0
    }

        SubShader{
            Pass {
                ZTest Always Cull Off ZWrite Off Fog { Mode off }

                CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
                #pragma target 3.0

                struct v2f
                {
                    float4 pos      : POSITION;
                    float2 uv       : TEXCOORD0;
                    float4 scr_pos  : TEXCOORD1;
                };

                uniform sampler2D _MainTex;
                uniform float _RedCoef;
                uniform float _GreenCoef;
                uniform float _BlueCoef;
                uniform float _Contrast;
                uniform float _Brightness;

                v2f vert(appdata_img v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
                    o.scr_pos = ComputeScreenPos(o.pos);
                    return o;
                }

                half4 frag(v2f i) : COLOR
                {
                    half4 color = tex2D(_MainTex, i.uv);
                    float4 muls = float4(0, 0, 0, 1);

                    float2 ps = (i.scr_pos.xy * _ScreenParams.xy / i.scr_pos.w) * 6;
                    uint px = (uint)ps.x % 6;
                    uint py = (uint)ps.y % 6;

                    color += (_Brightness / 255);
                    color = color - _Contrast * (color - 1.0) * color * (color - 0.5);

                    if (px == 5 || py == 5 )
                    {
                        color.r = _RedCoef;
                        color.g = _GreenCoef;
                        color.b = _BlueCoef;
                    }
                    else
                    {
                        color.r = _RedCoef * color.r;
                        color.g = _GreenCoef * color.g;
                        color.b = _BlueCoef * color.b;
                    }

                    return color;
                }

                ENDCG
            }
    }
        FallBack "Diffuse"
}