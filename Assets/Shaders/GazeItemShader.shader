Shader "Custom/GazeItem" {
    Properties {
        _MainTex        ("Base (RGB) Alpha (A)" ,          2D       )       = "white" {}
        _Color          ("Main Color"           ,       Color       )       = (1,1,1,1)
        _Selected       ("Select Percent"       ,       Range(0,1)  )       = 0
    }

    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D   _MainTex;
                float4      _MainTex_ST;
                
                float4      _Color;
                
                float       _Selected;
                float4      _SelectedColor;

                struct vertexOutput {
                    float4 pos      : SV_POSITION;
                    float2 uv       : TEXCOORD0;
                };

                vertexOutput vert (appdata_full input) {
                    vertexOutput output;
                    output.pos  = mul(UNITY_MATRIX_MVP, input.vertex);
                    output.uv   = TRANSFORM_TEX(input.texcoord, _MainTex);
                    return output;
                }
                
                float4 frag(vertexOutput input) : COLOR {
                    float4 c = _Color;
                    float4 cc = float4(1,1,1,1)-_Color;
                    cc.a = 1;
                    
                    if (input.uv.y < _Selected)
                    {
                        c = cc;
                    } else {
                        c = c;
                    }
                    
                    return c;
                }
            ENDCG
        }
    }
    
    FallBack "Unlit/UnlitAlphaWithFade"
}