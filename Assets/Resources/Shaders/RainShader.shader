Shader "Custom/RainEffect"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _DripRatio("�H�銄��", Range(0,1)) = 0.5
        _MinSize("�ŏ��J���T�C�Y", Float) = 0.05
        _MaxSize("�ő�J���T�C�Y", Float) = 0.15
        _RaindropCount("�J���̐�", Int) = 50
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
            }

            Cull Off
            ZWrite Off
            Fog { Mode Off }
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float _DripRatio;
                float _MinSize;
                float _MaxSize;
                int _RaindropCount;

                // ���������֐��i�����Ȃ��j
                float rand(float seed)
                {
                    return frac(sin(seed) * 43758.5453);
                }

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 pos : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float4 color = tex2D(_MainTex, i.uv);

                    // �J���̃��[�v
                    for (int j = 0; j < _RaindropCount; j++)
                    {
                        // �e�J���̃����_���ʒu�ƃT�C�Y�i���Ԃ������ē��I�ɕω��j
                        float seed = float(j);
                        float2 raindropPos = float2(rand(seed), rand(seed * 1.3));

                        // �H��J���͏��X��y�������Ɉړ�
                        if (rand(seed * 3.1) < _DripRatio)
                        {
                            raindropPos.y -= frac(_Time.y * 0.1); // ���X��y��������
                            raindropPos.y = fmod(raindropPos.y + 1.0, 1.0); // ��ʒ[�ɒB������Ăяォ�猻���
                        }

                        float size = lerp(_MinSize, _MaxSize, rand(seed * 2.1));
                        float dist = distance(i.uv, raindropPos);

                        if (dist < size)
                        {
                            // ���܌��ʂ𒲐�����UV�I�t�Z�b�g�𖾊m��
                            float refractionStrength = 0.05; // ���܂̋����𒲐�
                            float2 refractedUV = i.uv + normalize(i.uv - raindropPos) * refractionStrength * (size - dist);

                            // �J���̃G���A�ŋ��܌��ʂ�K�p
                            color.rgb = tex2D(_MainTex, refractedUV).rgb;
                        }
                    }
                    return color;
                }
                ENDCG
            }
        }
}