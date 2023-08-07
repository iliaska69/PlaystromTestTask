Shader "Custom/StencilMask"
{
    SubShader
    {
        Tags{"Queue" = "Transparent+1"}
        
        Pass
        {
            Blend Zero One
        }
    }
}
