Shader "Unlit/CausticsShader"
{
    // The properties block of the Unity shader. In this example this block is empty
    // because the output color is predefined in the fragment shader code.
    Properties
    { 
        _CausticsTexture ("Custom Caustics Texture", 2D) = ""{}
        _Caustics_ST("Caustics ST", Vector) = (1,1,0,0)
        _CausticsSpeed ("Caustics speed", Float) = 0.5
        _Color("Color (RGBA)", Color) = (0, 0, 1, 0.1) // add _Color property
    }

    // The SubShader block containing the Shader code.
    SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        Tags {"RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            // The HLSL code block. Unity SRP uses the HLSL language.
            HLSLPROGRAM
            // This line defines the name of the vertex shader.
            #pragma vertex vert
            // This line defines the name of the fragment shader.
            #pragma fragment frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // The DeclareDepthTexture.hlsl file contains utilities for sampling the Camera
            // depth texture.
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes
            {
                // The positionOS variable contains the vertex positions in object
                // space.
                float4 positionOS   : POSITION;
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
            };

            half4x4 _MainLightDirection;
            float4 _Color;
            float4 _Caustics_ST;
            float _CausticsSpeed;
            TEXTURE2D(_CausticsTexture);
            SAMPLER(sampler_CausticsTexture);


            // The vertex shader definition with properties defined in the Varyings
            // structure. The type of the vert function must match the type (struct)
            // that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT;
                // The TransformObjectToHClip function transforms vertex positions
                // from object space to homogenous clip space.
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                // Returning the output.
                return OUT;
            }

            half2 Panner(half2 uv, half speed, half tiling)
            {
                return (half2(1, 0) * _Time.y * speed) + uv;
            }

            // The fragment shader definition.
            half4 frag(Varyings IN) : SV_Target
            {
                // clip space
                float2 UV = IN.positionHCS.xy / _ScaledScreenParams.xy;
                // find depth
                #if UNITY_REVERSED_Z
                    real depth = SampleSceneDepth(UV);
                #else
                    // Adjust z to match NDC for OpenGL
                    real depth = lerp(UNITY_NEAR_CLIP_VALUE, 1, SampleSceneDepth(UV));
                #endif
                // Reconstruct world space positions from the UV and Z coordinates of pixels.
                float3 worldPos = ComputeWorldSpacePosition(UV, depth, UNITY_MATRIX_I_VP);
                // calculate position in object-space coordinates
                float3 objectSpacePos = TransformWorldToObject(worldPos);
                // box
                float boundingBoxMask = all(step(objectSpacePos, 0.5) * (1 - step(objectSpacePos, -0.5)));
                // TODO: Defining the color variable and returning it.
                half4 color = half4(0, 0, 0, 0);
                // calculate caustics texture UV coordinates (influenced by light direction)
                half2 uvCaustics = worldPos.xz * _Caustics_ST.xy + _Caustics_ST.zw;
                half2 moving_uv = Panner(uvCaustics, _CausticsSpeed, _Caustics_ST.zw);
                half4 caustics = SAMPLE_TEXTURE2D(_CausticsTexture, sampler_CausticsTexture, moving_uv);
                caustics.a = 0.3;
                // final color
                half4 finalColor = (color + caustics);
                finalColor.a = 0.25;
                // create bounding box mask
                return  caustics * boundingBoxMask; //*boundingBoxMask;
            }
            ENDHLSL
        }
    }
}
