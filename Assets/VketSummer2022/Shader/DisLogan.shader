// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32809,y:31936,varname:node_4013,prsc:2|emission-7908-OUT,custl-5162-RGB,clip-3743-OUT;n:type:ShaderForge.SFN_Tex2d,id:4767,x:31898,y:31791,ptovrint:False,ptlb:EemissionTex,ptin:_EemissionTex,varname:node_4767,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ViewPosition,id:2286,x:31870,y:32117,varname:node_2286,prsc:2;n:type:ShaderForge.SFN_Distance,id:4230,x:32264,y:32183,varname:node_4230,prsc:2|A-2286-XYZ,B-8652-XYZ;n:type:ShaderForge.SFN_FragmentPosition,id:8652,x:31870,y:32235,varname:node_8652,prsc:2;n:type:ShaderForge.SFN_Divide,id:3743,x:32510,y:32183,varname:node_3743,prsc:2|A-1674-OUT,B-4230-OUT;n:type:ShaderForge.SFN_Multiply,id:7908,x:32513,y:31867,varname:node_7908,prsc:2|A-8854-OUT,B-4767-RGB;n:type:ShaderForge.SFN_Slider,id:8854,x:32173,y:31774,ptovrint:False,ptlb:Transparency,ptin:_Transparency,varname:node_8854,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Slider,id:1674,x:32164,y:32087,ptovrint:False,ptlb:Distance,ptin:_Distance,varname:node_1674,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.2,max:100;n:type:ShaderForge.SFN_Tex2d,id:5162,x:32499,y:32004,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5162,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:5162-1674-4767-8854;pass:END;sub:END;*/

Shader "ShinJusty/DisLogan" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Distance ("Distance", Range(0, 100)) = 1.2
        _EemissionTex ("EemissionTex", 2D) = "white" {}
        _Transparency ("Transparency", Range(0, 1)) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _EemissionTex; uniform float4 _EemissionTex_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Transparency)
                UNITY_DEFINE_INSTANCED_PROP( float, _Distance)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float _Distance_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Distance );
                clip((_Distance_var/distance(_WorldSpaceCameraPos,i.posWorld.rgb)) - 0.5);
////// Lighting:
////// Emissive:
                float _Transparency_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Transparency );
                float4 _EemissionTex_var = tex2D(_EemissionTex,TRANSFORM_TEX(i.uv0, _EemissionTex));
                float3 emissive = (_Transparency_var*_EemissionTex_var.rgb);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 finalColor = emissive + _MainTex_var.rgb;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Distance)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float _Distance_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Distance );
                clip((_Distance_var/distance(_WorldSpaceCameraPos,i.posWorld.rgb)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
