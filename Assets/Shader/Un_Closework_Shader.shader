Shader "Custom/FrameMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // 主纹理
        _RectCenter ("Rect Center", Vector) = (0.5, 0.5, 0, 0) // 矩形中心
        _RectWidth ("Rect Width", Range(0, 1)) = 0.5 // 矩形宽度
        _RectHeight ("Rect Height", Range(0, 1)) = 0.5 // 矩形高度
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
                float4 vertex : POSITION; // 顶点位置
                float2 texcoord : TEXCOORD0; // 纹理坐标
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0; // 传递到片段着色器的纹理坐标
                float4 vertex : SV_POSITION; // 传递到片段着色器的顶点位置
            };

            sampler2D _MainTex; // 纹理采样器
            float4 _MainTex_ST; // 纹理变换
            float2 _RectCenter; // 矩形中心
            float _RectWidth; // 矩形宽度
            float _RectHeight; // 矩形高度

            // 顶点着色器
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // 转换为裁剪空间坐标
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex); // 变换纹理坐标
                return o;
            }

            // 片段着色器
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.texcoord; // 获取片段的纹理坐标
                float2 rectCenter = _RectCenter; // 矩形中心
                float halfWidth = _RectWidth / 2.0; // 矩形一半宽度
                float halfHeight = _RectHeight / 2.0; // 矩形一半高度

                // 计算当前像素相对于矩形中心的绝对距离
                float2 dist = abs(uv - rectCenter);

                // 如果当前像素在矩形内，则显示该像素，否则显示透明
                if (dist.x < halfWidth && dist.y < halfHeight)
                {
                    return tex2D(_MainTex, uv); // 返回纹理颜色
                }
                else
                {
                    return fixed4(0, 0, 0, 0); // 返回透明
                }
            }
            ENDCG
        }
    }
}
