Shader "FX/SeeThrough" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OccludeColor ("Occlusion Color", Color) = (0,0,1,1)
    }

    SubShader {
        Tags {"Queue"="Geometry+5"}
        // �������� ������: ���������� ����������, ����� ������ ��������� �����
        Pass {
            ZWrite Off              // ������ � ����� ������� ���������
            ZTest Greater           // ��������� �������: ���������� ������ ���� ��������
            Cull Back               // ��������� ������� �������� (����� ������ ������ �������)
            Blend One Zero           // ������� ��������� ������
            Color [_OccludeColor]   // �������� ���� ������� �� ������������
        }
    }
    FallBack "Hidden"
}