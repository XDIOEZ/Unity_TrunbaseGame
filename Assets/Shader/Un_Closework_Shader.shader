Shader "Custom/FrameMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // ������
        _RectCenter ("Rect Center", Vector) = (0.5, 0.5, 0, 0) // ��������
        _RectWidth ("Rect Width", Range(0, 1)) = 0.5 // ���ο��
        _RectHeight ("Rect Height", Range(0, 1)) = 0.5 // ���θ߶�
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION; // ����λ��
                float2 texcoord : TEXCOORD0; // ��������
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0; // ���ݵ�Ƭ����ɫ������������
                float4 vertex : SV_POSITION; // ���ݵ�Ƭ����ɫ���Ķ���λ��
            };

            sampler2D _MainTex; // ���������
            float4 _MainTex_ST; // ����任
            float2 _RectCenter; // ��������
            float _RectWidth; // ���ο��
            float _RectHeight; // ���θ߶�

            // ������ɫ��
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // ת��Ϊ�ü��ռ�����
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex); // �任��������
                return o;
            }

            // Ƭ����ɫ��
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.texcoord; // ��ȡƬ�ε���������
                float2 rectCenter = _RectCenter; // ��������
                float halfWidth = _RectWidth / 2.0; // ����һ����
                float halfHeight = _RectHeight / 2.0; // ����һ��߶�

                // ���㵱ǰ��������ھ������ĵľ��Ծ���
                float2 dist = abs(uv - rectCenter);

                // �����ǰ�����ھ����ڣ�����ʾ�����أ�������ʾ͸��
                if (dist.x < halfWidth && dist.y < halfHeight)
                {
                    return tex2D(_MainTex, uv); // ����������ɫ
                }
                else
                {
                    return fixed4(0, 0, 0, 0); // ����͸��
                }
            }
            ENDCG
        }
    }
}
