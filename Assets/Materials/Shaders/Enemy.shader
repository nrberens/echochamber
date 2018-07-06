// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-5233-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32010,y:32535,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9264706,c2:0.1569175,c3:0.06812285,c4:1;n:type:ShaderForge.SFN_Fresnel,id:7046,x:31803,y:32906,varname:node_7046,prsc:2;n:type:ShaderForge.SFN_Power,id:3311,x:31988,y:32906,varname:node_3311,prsc:2|VAL-7046-OUT,EXP-6455-OUT;n:type:ShaderForge.SFN_Vector1,id:6455,x:31803,y:33058,varname:node_6455,prsc:2,v1:2.5;n:type:ShaderForge.SFN_Add,id:2499,x:32151,y:32906,varname:node_2499,prsc:2|A-3311-OUT,B-3311-OUT;n:type:ShaderForge.SFN_Multiply,id:396,x:32346,y:32906,varname:node_396,prsc:2|A-1472-RGB,B-2499-OUT;n:type:ShaderForge.SFN_Color,id:1472,x:32010,y:32731,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.8602941,c3:0.8602941,c4:1;n:type:ShaderForge.SFN_Add,id:5233,x:32397,y:32650,varname:node_5233,prsc:2|A-7241-RGB,B-396-OUT;proporder:7241-1472;pass:END;sub:END;*/

Shader "Shader Forge/Enemy" {
    Properties {
        _Color ("Color", Color) = (0.9264706,0.1569175,0.06812285,1)
        _Color2 ("Color2", Color) = (1,0.8602941,0.8602941,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float4 _Color2;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_3311 = pow((1.0-max(0,dot(normalDirection, viewDirection))),2.5);
                float3 emissive = (_Color.rgb+(_Color2.rgb*(node_3311+node_3311)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
