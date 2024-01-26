// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33001,y:32765,varname:node_4013,prsc:2|emission-6906-OUT,custl-1134-RGB,alpha-267-OUT,clip-1134-A,refract-4523-OUT;n:type:ShaderForge.SFN_Lerp,id:6906,x:32544,y:32876,varname:node_6906,prsc:2|A-819-RGB,B-975-RGB,T-267-OUT;n:type:ShaderForge.SFN_Color,id:819,x:32357,y:32658,ptovrint:False,ptlb:Color1,ptin:_Color1,varname:node_819,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8160377,c2:0.825005,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:975,x:32294,y:32876,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_975,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6650944,c2:0.8604932,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:267,x:32424,y:33086,varname:node_267,prsc:2|EXP-8580-OUT;n:type:ShaderForge.SFN_Slider,id:8580,x:32189,y:33298,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_8580,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:3;n:type:ShaderForge.SFN_Multiply,id:5819,x:32688,y:33378,varname:node_5819,prsc:2|A-267-OUT,B-4572-OUT;n:type:ShaderForge.SFN_Slider,id:4572,x:32224,y:33416,ptovrint:False,ptlb:Reflect,ptin:_Reflect,varname:node_4572,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.31,max:3;n:type:ShaderForge.SFN_Multiply,id:4523,x:32878,y:33531,varname:node_4523,prsc:2|A-3264-UVOUT,B-5819-OUT;n:type:ShaderForge.SFN_TexCoord,id:3264,x:32536,y:33545,varname:node_3264,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:1134,x:32639,y:32688,ptovrint:False,ptlb:MeinTex,ptin:_MeinTex,varname:node_1134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:False;proporder:1134-819-975-8580-4572;pass:END;sub:END;*/

Shader "ShinJusty/Ice_ZwriteOFF" {
    Properties {
        _MeinTex ("MeinTex", 2D) = "bump" {}
        _Color1 ("Color1", Color) = (0.8160377,0.825005,1,1)
        _Color2 ("Color2", Color) = (0.6650944,0.8604932,1,1)
        _Fresnel ("Fresnel", Range(0, 3)) = 2
        _Reflect ("Reflect", Range(0, 3)) = 0.31
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MeinTex; uniform float4 _MeinTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color1)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color2)
                UNITY_DEFINE_INSTANCED_PROP( float, _Fresnel)
                UNITY_DEFINE_INSTANCED_PROP( float, _Reflect)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float _Fresnel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Fresnel );
                float node_267 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_var);
                float _Reflect_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Reflect );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (i.uv0*(node_267*_Reflect_var));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float4 _MeinTex_var = tex2D(_MeinTex,TRANSFORM_TEX(i.uv0, _MeinTex));
                clip(_MeinTex_var.a - 0.5);
////// Lighting:
////// Emissive:
                float4 _Color1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color1 );
                float4 _Color2_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color2 );
                float3 emissive = lerp(_Color1_var.rgb,_Color2_var.rgb,node_267);
                float3 finalColor = emissive + _MeinTex_var.rgb;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,node_267),1);
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
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _MeinTex; uniform float4 _MeinTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _MeinTex_var = tex2D(_MeinTex,TRANSFORM_TEX(i.uv0, _MeinTex));
                clip(_MeinTex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
