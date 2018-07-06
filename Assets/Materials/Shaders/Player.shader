// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:True,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-6416-OUT,voffset-2344-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31704,y:32419,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7720588,c2:0.981136,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2344,x:31710,y:33252,varname:node_2344,prsc:2|A-9350-OUT,B-8400-OUT;n:type:ShaderForge.SFN_NormalVector,id:8400,x:31513,y:33325,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector1,id:5493,x:31123,y:33089,varname:node_5493,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Sin,id:8599,x:31056,y:33178,varname:node_8599,prsc:2|IN-8425-TDB;n:type:ShaderForge.SFN_Time,id:8425,x:30856,y:33178,varname:node_8425,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1519,x:31321,y:33105,varname:node_1519,prsc:2|A-5493-OUT,B-8599-OUT;n:type:ShaderForge.SFN_Power,id:9350,x:31513,y:33105,varname:node_9350,prsc:2|VAL-1519-OUT,EXP-6604-OUT;n:type:ShaderForge.SFN_Vector1,id:6604,x:31297,y:33244,varname:node_6604,prsc:2,v1:1.15;n:type:ShaderForge.SFN_Lerp,id:910,x:31924,y:32625,varname:node_910,prsc:2|A-7241-RGB,B-6576-RGB,T-8883-OUT;n:type:ShaderForge.SFN_Color,id:6576,x:31704,y:32625,ptovrint:False,ptlb:Color_2,ptin:_Color_2,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5216263,c2:0.5823827,c3:0.9852941,c4:1;n:type:ShaderForge.SFN_Cos,id:7737,x:31257,y:32639,varname:node_7737,prsc:2|IN-8425-TDB;n:type:ShaderForge.SFN_Multiply,id:8883,x:31453,y:32639,varname:node_8883,prsc:2|A-7737-OUT,B-8599-OUT;n:type:ShaderForge.SFN_Fresnel,id:5954,x:31762,y:32875,varname:node_5954,prsc:2;n:type:ShaderForge.SFN_Add,id:6416,x:32333,y:32706,varname:node_6416,prsc:2|A-910-OUT,B-5042-OUT;n:type:ShaderForge.SFN_Multiply,id:5042,x:32166,y:32781,varname:node_5042,prsc:2|A-2031-OUT,B-2264-OUT;n:type:ShaderForge.SFN_Vector3,id:2031,x:31766,y:32795,varname:node_2031,prsc:2,v1:0.734485,v2:0.3278006,v3:0.9485294;n:type:ShaderForge.SFN_Power,id:2264,x:31967,y:32875,varname:node_2264,prsc:2|VAL-5954-OUT,EXP-6027-OUT;n:type:ShaderForge.SFN_Vector1,id:6027,x:31766,y:33029,varname:node_6027,prsc:2,v1:2;proporder:7241-6576;pass:END;sub:END;*/

Shader "Shader Forge/Player" {
    Properties {
        _Color ("Color", Color) = (0.7720588,0.981136,1,1)
        _Color_2 ("Color_2", Color) = (0.5216263,0.5823827,0.9852941,1)
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
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float4 _Color_2;
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
                float4 node_8425 = _Time + _TimeEditor;
                float node_8599 = sin(node_8425.b);
                v.vertex.xyz += (pow((0.1*node_8599),1.15)*v.normal);
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
                float4 node_8425 = _Time + _TimeEditor;
                float node_8599 = sin(node_8425.b);
                float3 emissive = (lerp(_Color.rgb,_Color_2.rgb,(cos(node_8425.b)*node_8599))+(float3(0.734485,0.3278006,0.9485294)*pow((1.0-max(0,dot(normalDirection, viewDirection))),2.0)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_8425 = _Time + _TimeEditor;
                float node_8599 = sin(node_8425.b);
                v.vertex.xyz += (pow((0.1*node_8599),1.15)*v.normal);
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
