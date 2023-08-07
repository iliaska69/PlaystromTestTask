// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteGradientShader" {
     Properties{
         [PerRendererData] MainTex("Sprite Texture", 2D) = "white" {}
         _Color("Left Color", Color) = (1,1,1,1)
         _Color2("Right Color", Color) = (1,1,1,1)
         [MaterialToggle] _IsVertical("Vertical Gradient", Float) = 0
         _Scale("Scale", Float) = 1
         
         // these six unused properties are required when a shader
         // is used in the UI system, or you get a warning.
         // look to UI-Default.shader to see these.
         _StencilComp("Stencil Comparison", Float) = 8
         _Stencil("Stencil ID", Float) = 0
         _StencilOp("Stencil Operation", Float) = 0
         _StencilWriteMask("Stencil Write Mask", Float) = 255
         _StencilReadMask("Stencil Read Mask", Float) = 255
         _ColorMask("Color Mask", Float) = 15
         // see for example
         // http://answers.unity3d.com/questions/980924/ui-mask-with-shader.html
 
     }
 
         SubShader{
         Tags{ "Queue" = "Transparent"
             "IgnoreProjector" = "True"
             "RenderType" = "Transparent"
             "PreviewType" = "Plane"
             "CanUseSpriteAtlas" = "True" }
     
 
         Stencil
         {
             Ref[_Stencil]
             Comp[_StencilComp]
             Pass[_StencilOp]
             ReadMask[_StencilReadMask]
             WriteMask[_StencilWriteMask]
         }
 
         Cull Off
         Lighting Off
         ZWrite Off
         ZTest[unity_GUIZTestMode]
         Fog{ Mode Off }
         Blend SrcAlpha OneMinusSrcAlpha
         ColorMask[_ColorMask]
 
         Pass{
         CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 #include "UnityCG.cginc"
 
         struct appdata_t
     {
         float4 vertex   : POSITION;
         float4 color    : COLOR;
         float2 texcoord : TEXCOORD0;
     };
 
     struct v2f
     {
         float4 vertex   : SV_POSITION;
         fixed4 color : COLOR;
         half2 texcoord  : TEXCOORD0;
     };
 
     fixed4 _Color;
     fixed4 _Color2;
     float _IsVertical;
         
     v2f vert(appdata_t IN)
     {
         v2f OUT;
         OUT.vertex = UnityObjectToClipPos(IN.vertex);
         OUT.texcoord = IN.texcoord;
 #ifdef UNITY_HALF_TEXEL_OFFSET
         OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
 #endif
         fixed4 dir = _IsVertical > 0.0 ? IN.texcoord.y : IN.texcoord.x;
         OUT.color = lerp(_Color, _Color2, dir);
         return OUT;
     }
 
     sampler2D _MainTex;
 
     fixed4 frag(v2f i) : COLOR{
         fixed4 c = tex2D(_MainTex, i.texcoord) * i.color;
         clip(c.a - 0.01);
         return c;
     }
         ENDCG
     }
     }
 }