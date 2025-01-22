Shader "Custom/BubbleUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Gradient ("Gradient", 2D) = "white" {} // Rainbow gradient texture
        _Transparency ("Transparency", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200

        Pass
        {
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _Gradient;
            float _Transparency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the main texture (your bubble sprite)
                fixed4 col = tex2D(_MainTex, i.uv);

                // Sample the gradient texture for the rainbow effect
                fixed4 gradient = tex2D(_Gradient, float2(i.uv.x, _Time.y));

                // Combine the gradient with the main texture
                col.rgb *= gradient.rgb;

                // Apply transparency
                col.a = _Transparency;

                return col;
            }
            ENDCG
        }
    }
}