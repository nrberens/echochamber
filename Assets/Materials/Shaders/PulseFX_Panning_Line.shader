// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2305-OUT,alpha-9705-OUT;n:type:ShaderForge.SFN_Tex2d,id:8012,x:31855,y:32800,ptovrint:False,ptlb:circle,ptin:_circle,varname:node_8012,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d18684a5e12e01b40b618372552d1f99,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:218,x:32146,y:32654,varname:node_218,prsc:2|A-2186-RGB,B-8012-B;n:type:ShaderForge.SFN_VertexColor,id:2186,x:31855,y:32627,varname:node_2186,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9705,x:32146,y:32805,varname:node_9705,prsc:2|A-2186-A,B-8012-B;n:type:ShaderForge.SFN_Multiply,id:2305,x:32459,y:32668,varname:node_2305,prsc:2|A-9418-OUT,B-218-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9418,x:32146,y:32549,ptovrint:False,ptlb:Emissive Strength,ptin:_EmissiveStrength,varname:node_9418,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:8012-9418;pass:END;sub:END;*/

Shader "Shader Forge/PulseFX_Panning_Line" {
    Properties {
        _circle ("circle", 2D) = "white" {}
        _EmissiveStrength ("Emissive Strength", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _circle; uniform float4 _circle_ST;
            uniform float _EmissiveStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _circle_var = tex2D(_circle,TRANSFORM_TEX(i.uv0, _circle));
                float3 emissive = (_EmissiveStrength*(i.vertexColor.rgb*_circle_var.b));
                float3 finalColor = emissive;
                return fixed4(finalColor,(i.vertexColor.a*_circle_var.b));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
