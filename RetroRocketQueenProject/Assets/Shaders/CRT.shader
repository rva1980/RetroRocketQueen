// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CRT"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}

        _MaxRedCoef("Max Red Coef", Float) = 1
        _MinRedCoef("Min Red Coef", Float) = 0.8
        _MaxGreenCoef("Max Green Coef", Float) = 1
        _MinGreenCoef("Min Green Coef", Float) = 0.8
        _MaxBlueCoef("Max Blue Coef", Float) = 1
        _MinBlueCoef("Min Blue Coef", Float) = 0.8

        _ScansCoef("Scans Coef", Float) = 0.8
        _ScansCount("Scans Count", Float) = 180

        _Contrast("Contrast", Float) = 0
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
                uniform float _MaxRedCoef;
                uniform float _MinRedCoef;
                uniform float _MaxGreenCoef;
                uniform float _MinGreenCoef;
                uniform float _MaxBlueCoef;
                uniform float _MinBlueCoef;
                uniform float _ScansCoef;
                uniform float _ScansCount;
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

                    float2 ps = (i.scr_pos.xy * _ScreenParams.xy / i.scr_pos.w) * 3;
                    uint pp = (uint)ps.x % 3;
                    float4 outcolor = float4(0, 0, 0, 1);

                    color += (_Brightness / 255);
                    color = color - _Contrast * (color - 1.0) * color * (color - 0.5);

                    if (pp == 0)
                    {
                        muls.r = _MaxRedCoef;
                        muls.g = _MinGreenCoef;
                        muls.b = _MinBlueCoef;
                    }
                    else if (pp == 1)
                    {
                        muls.r = _MinRedCoef;
                        muls.g = _MaxGreenCoef;
                        muls.b = _MinBlueCoef;
                    }
                    else
                    {
                        muls.r = _MinRedCoef;
                        muls.g = _MinGreenCoef;
                        muls.b = _MaxBlueCoef;
                    }

                    float s = - cos(i.scr_pos.y * _ScansCount * 3.14159265 * 2);
                    s = (s * 0.5 + 0.5) * _ScansCoef + (1 - _ScansCoef);
                    s = pow(s, 0.5);
                    muls *= float4(s, s, s, 1);
                    
                    color = color * muls;

                    return color;
                }

                ENDCG
            }
    }
        FallBack "Diffuse"
}