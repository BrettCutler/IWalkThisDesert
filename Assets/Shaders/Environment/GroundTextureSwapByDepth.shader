// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33927,y:32704,varname:node_1,prsc:2|emission-14-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33453,y:32365,ptovrint:False,ptlb:NearTexture,ptin:_NearTexture,varname:node_1762,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3,x:33264,y:32859,ptovrint:False,ptlb:MidTexture,ptin:_MidTexture,varname:node_2305,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4,x:33274,y:33052,ptovrint:False,ptlb:FarTexture,ptin:_FarTexture,varname:node_9174,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Depth,id:5,x:32950,y:32300,varname:node_5,prsc:2;n:type:ShaderForge.SFN_SceneDepth,id:6,x:32666,y:32350,varname:node_6,prsc:2;n:type:ShaderForge.SFN_Multiply,id:14,x:33729,y:32541,varname:node_14,prsc:2|A-2-RGB,B-2179-OUT;n:type:ShaderForge.SFN_Divide,id:3766,x:32912,y:32520,varname:node_3766,prsc:2|A-6-OUT,B-7931-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7931,x:32664,y:32510,ptovrint:False,ptlb:CameraViewDistance,ptin:_CameraViewDistance,varname:node_7931,prsc:2,glob:False,v1:1000;n:type:ShaderForge.SFN_ValueProperty,id:4730,x:32912,y:32718,ptovrint:False,ptlb:NearCutoff,ptin:_NearCutoff,varname:node_4730,prsc:2,glob:False,v1:0.3;n:type:ShaderForge.SFN_Clamp,id:845,x:33133,y:32550,varname:node_845,prsc:2|IN-3766-OUT,MIN-9577-OUT,MAX-4730-OUT;n:type:ShaderForge.SFN_Vector1,id:9577,x:32912,y:32638,varname:node_9577,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:2179,x:33347,y:32550,varname:node_2179,prsc:2|A-845-OUT,B-7994-OUT;n:type:ShaderForge.SFN_Vector1,id:2793,x:33132,y:32722,varname:node_2793,prsc:2,v1:1;n:type:ShaderForge.SFN_Divide,id:7994,x:33380,y:32688,varname:node_7994,prsc:2|A-2793-OUT,B-4730-OUT;proporder:2-7931-4730;pass:END;sub:END;*/

Shader "Custom/GroundTextureSwapByDepth" {
    Properties {
        _NearTexture ("NearTexture", 2D) = "white" {}
        _CameraViewDistance ("CameraViewDistance", Float ) = 1000
        _NearCutoff ("NearCutoff", Float ) = 0.3
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _NearTexture; uniform float4 _NearTexture_ST;
            uniform float _CameraViewDistance;
            uniform float _NearCutoff;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD3;
                #else
                    float3 shLight : TEXCOORD3;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _NearTexture_var = tex2D(_NearTexture,TRANSFORM_TEX(i.uv0, _NearTexture));
                float node_3766 = (sceneZ/_CameraViewDistance);
                float3 emissive = (_NearTexture_var.rgb*(clamp(node_3766,0.0,_NearCutoff)*(1.0/_NearCutoff)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
