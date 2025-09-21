using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SmoothColorTransition : MonoBehaviour
{
    public Light targetLight;       // Цель света, цвет которого меняется
    public Color startColor;          // Первый цвет
    public Color endColor;          // Последний цвет
    public List<Color> Colors = new List<Color>();        // цвета
    public Color curColor;          // Текущий цвет
    public float transitionTime = 5f; // Продолжительность изменения цвета (таймер)
    public float passedTime = 0f;
    public bool isTransitioning = true;
    public bool isFirst = true;

    void Update()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (isTransitioning)
        {
            if (isFirst)
            {
                StartTimer();
            }
            else if (passedTime == transitionTime)
            {
                FinishTimer();
            }
            else
            {
                WorkingTimer();
            }
            yield return new WaitForSeconds(1f); // Пауза на одну секунду
            passedTime += 1f;
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
        
    }

    public void FinishTimer()
    {
        targetLight.color = endColor;
        curColor = targetLight.color;
        isTransitioning = false;
    }

    public void AddColors()
    {
        transitionTime -= 2;
        Color c = Color.black;
        for (float i = 0; i < transitionTime; i+=1)
        {
            if (i == 0)
            {
                c = Color.Lerp(startColor, endColor, 0.5f);
            }
            else if (i % 2 == 0)
            {

            }
            else if (i % 2 != 1)
            {

            }
            Colors[(int)i] = c;
        }
    }
}