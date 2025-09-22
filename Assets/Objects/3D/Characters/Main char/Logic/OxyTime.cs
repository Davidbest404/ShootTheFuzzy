using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SmoothColorTransition : MonoBehaviour
{
    public bool isTransitioning = true;
    public bool isFirst = true;
    public Light targetLight;       // ���� �����, ���� �������� ��������
    public Color startColor;          // ������ ����
    public Color endColor;          // ��������� ����
    public Color curColor;          // ������� ����
    public List<Color> Colors = new List<Color>();        // �����
    public float transitionTime = 5f; // ����������������� ��������� ����� (������)
    public float passedTime = 0f;
    private float Sec;

    public bool Turn;
    public int Progress;

    private void Start()
    {
        transitionTime = RoundTimer(transitionTime);
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

    public void AddColors()
    {
        // ������� ������ ������
        Colors.Clear();

        Colors.Add(startColor);

        for (int i = 0; i < transitionTime - 2; i++)
        {
            Colors.Add(Color.black);
        }

        Colors.Add(endColor);

        int index = (int)transitionTime;
        int indexTurn = 0;

        for (int i = 0; i < transitionTime; i++)
        {
            if (i == 0)
            {
                Colors[index / 2] = Color.Lerp(startColor, endColor, 0.5f);
                index = index / 2;
                indexTurn = 1;
            }
            else if (index != 1 && indexTurn == 1)
            {
                index = index / 2;
                if (index == 1)
                {
                    indexTurn = 2;
                    i--;
                    index = (int)transitionTime;
                }
                else
                {
                    Colors[index] = Color.Lerp(startColor, Colors[index * 2], 0.5f);
                }
            }
            if (index != 1 && indexTurn == 2)
            {
                int blackIndex = Colors.FindIndex(color => color == Color.black);
                Colors[FindMiddleBlackInSequence(Colors)] = Color.Lerp(
                    Colors[blackIndex - 1],
                    Colors.Skip(blackIndex + 1).FirstOrDefault(color => color != Color.black),
                    0.5f);
            }
        }
    }

    public float RoundTimer(float timerTime)
    {
        if (timerTime <= 3) return 3f;

        float previous = 3f; // ������ ������� ������������������ ����� ���������
        float step = 2f;     // ������� ����� (������� ������)

        while (previous <= timerTime)
        {
            previous += Mathf.Pow(2f, step); // ��������� ��������� ������� ������
            step++;
        }

        return previous + 2;
    }

    public static int FindMiddleBlackInSequence(List<Color> colorsList)
    {
        bool isFindingBlacks = false;
        int startIndexOfBlacks = -1;
        int countBlacks = 0;

        for (int i = 0; i < colorsList.Count; i++)
        {
            if (colorsList[i] == Color.black)
            {
                if (!isFindingBlacks)
                {
                    // ������ ������������������ ������ ������
                    startIndexOfBlacks = i;
                    isFindingBlacks = true;
                }
                countBlacks++;
            }
            else
            {
                if (isFindingBlacks)
                {
                    // ����� ������ ������ ������ ������
                    break;
                }
            }
        }

        if (countBlacks > 0)
        {
            // ������� ������ �������� ����� ���������������� ������ ������
            int middleIndex = startIndexOfBlacks + (countBlacks - 1) / 2;
            return middleIndex;
        }
        else
        {
            return 0;
        }
    }
}