using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OxyTime : MonoBehaviour
{
    [SerializeField] public bool isTransitioning = true;

    [SerializeField] public float transitionTime = 5f; // ����������������� ��������� ����� (������)
    [SerializeField] private float Sec;
    [SerializeField] private int passedTime = 0;
    [SerializeField] public int Progress = 0;

    [SerializeField] private Material targetMat;       // ���� �����, ���� �������� ��������
    [SerializeField] private Color startColor;          // ������ ����
    [SerializeField] private Color endColor;          // ��������� ����
    [SerializeField] private Color curColor;          // ������� ����
    
    public void Update()
    {
        if (isTransitioning)
        {
            Sec += Time.deltaTime;

            while (Sec >= 1f)
            {
                WorkingTimer();

                if (Progress >= transitionTime)
                {
                    isTransitioning = false;
                    passedTime = 0;
                }
                Sec -= 1f;
                Progress++;
            }
        }
    }

    public void WorkingTimer()
    {
        // ������������ ���� ����������� �������
        float normalizedTime = Progress / transitionTime;

        // ������������ ���������� ����������� � �������� �� 0 �� 1
        normalizedTime = Mathf.Clamp01(normalizedTime);

        targetMat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, normalizedTime));
        curColor = targetMat.GetColor("_EmissionColor");
        passedTime = Progress;
    }
}