using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SmoothColorTransition : MonoBehaviour
{
    public bool isTransitioning = true;
    public bool isFirst = true;
    public Light targetLight;       // ���� �����, ���� �������� ��������
    public Color startColor;          // ������ ����
    public Color endColor;          // ��������� ����
    public List<Color> startColors = new List<Color>();        // �����
    public Color midColor;        // �����
    public List<Color> endColors = new List<Color>();        // �����
    public Color curColor;          // ������� ����
    public float transitionTime = 5f; // ����������������� ��������� ����� (������)
    public float passedTime = 0f;
    private float Sec;

    public bool Turn;
    public int Progress;

    private void Start()
    {
        AddColors();
    }

    public void Update()
    {
        Sec += Time.deltaTime;

        if (isTransitioning)
        {
            while (Sec >= 1f)
            {
                if (isFirst)
                {
                    StartTimer();
                }
                else if (passedTime >= transitionTime)
                {
                    FinishTimer();
                }
                else
                {
                    WorkingTimer();
                }
                Sec -= 1f;
                passedTime++;
            }
        }
    }

    public void StartTimer()
    {
        targetLight.color = startColor;
        curColor = targetLight.color;
        isFirst = false;
    }

    public void WorkingTimer()
    {
        if (Turn)
        {

        }
    }

    public void FinishTimer()
    {
        targetLight.color = endColor;
        curColor = targetLight.color;
        isTransitioning = false;
    }

    public void AddColors()         //  ---------
    {
        midColor = Color.Lerp(startColor, endColor, 0.5f);

        if (transitionTime % 2 == 0)
        {
            for (int i = 0; i < (transitionTime - 2) / 2; i++)
            {
                startColors.Add(startColor);
            }

            for (int i = 0; i < (transitionTime - 2) / 2; i++)
            {
                startColors.Add(endColor);
            }

            Progress = startColors.Count + endColors.Count;
        }
        else
        {
            for (int i = 0; i < (transitionTime - 3) / 2; i++)
            {
                startColors.Add(startColor);
            }

            for (int i = 0; i < (transitionTime - 3) / 2; i++)
            {
                startColors.Add(endColor);
            }

            Progress = startColors.Count + endColors.Count + 1;
        }
        //  ---------
        Color c = Color.white;
        for (int i = 0; i < transitionTime - 2; i++)
        {

        }
    }
}