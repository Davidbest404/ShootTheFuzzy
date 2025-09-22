using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SmoothColorTransition : MonoBehaviour
{
    public bool isTransitioning = true;
    public bool isFirst = true;
    public Light targetLight;       // Цель света, цвет которого меняется
    public Color startColor;          // Первый цвет
    public Color endColor;          // Последний цвет
    public List<Color> startColors = new List<Color>();        // цвета
    public Color midColor;        // цвета
    public List<Color> endColors = new List<Color>();        // цвета
    public Color curColor;          // Текущий цвет
    public float transitionTime = 5f; // Продолжительность изменения цвета (таймер)
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