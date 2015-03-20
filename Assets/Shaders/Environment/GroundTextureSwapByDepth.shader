// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-14-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33312,y:32365,ptlb:NearTexture,ptin:_NearTexture,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3,x:33501,y:32859,ptlb:MidTexture,ptin:_MidTexture,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4,x:33491,y:33052,ptlb:FarTexture,ptin:_FarTexture,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Depth,id:5,x:33598,y:32584;n:type:ShaderForge.SFN_SceneDepth,id:6,x:33598,y:32464;n:type:ShaderForge.SFN_Multiply,id:14,x:33189,y:32612|A-2-RGB,B-32-OUT;n:type:ShaderForge.SFN_Vector1,id:19,x:33598,y:32713,v1:0.5;n:type:ShaderForge.SFN_Clamp01,id:32,x:33402,y:32612|IN-6-OUT;proporder:2;pass:END;sub:END;*/

Shader "Custom/GroundTextureSwapByDepth" {
    Properties {
        _NearTexture ("NearTexture", 2D) = "white" {}
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _NearTexture; uniform float4 _NearTexture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float2 node_40 = i.uv0;
                float3 emissive = (tex2D(_NearTexture,TRANSFORM_TEX(node_40.rg, _NearTexture)).rgb*saturate(sceneZ));
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
