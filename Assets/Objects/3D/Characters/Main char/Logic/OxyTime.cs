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

    [SerializeField] public float transitionTime = 5f; // Продолжительность изменения цвета (таймер)
    [SerializeField] private float Sec;
    [SerializeField] private int passedTime = 0;
    [SerializeField] public int Progress = 0;

    [SerializeField] private Material targetMat;       // Цель света, цвет которого меняется
    [SerializeField] private Color startColor;          // Первый цвет
    [SerializeField] private Color endColor;          // Последний цвет
    [SerializeField] private Color curColor;          // Текущий цвет

    [SerializeField] public CorpsesSpawner Corpse;

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
                    float normalizedTime = Progress / transitionTime;
                    normalizedTime = Mathf.Clamp01(normalizedTime);

                    targetMat.SetColor("_EmissionColor", startColor);
                    curColor = targetMat.GetColor("_EmissionColor");

                    passedTime = 0;
                    Progress = 0;

                    Corpse.SpawnAndMove();
                }
                Sec -= 1f;
                Progress++;
            }
        }
    }

    public void WorkingTimer()
    {
        float normalizedTime = Progress / transitionTime;
        normalizedTime = Mathf.Clamp01(normalizedTime);

        targetMat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, normalizedTime));
        curColor = targetMat.GetColor("_EmissionColor");
        passedTime = Progress;
    }
}