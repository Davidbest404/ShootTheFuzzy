using UnityEngine;
using System;

[Serializable]
public class ObjectProceduralAnimation
{
    public PropertyType PropertyType;
    public float TimeOffset = 0;
    public float Scale;
    public AnimationCurve Curve;

    public float Evaluate(float normalizedTime)
    {
        return Curve.Evaluate(normalizedTime + TimeOffset) * Scale;
    }
}

public enum PropertyType
{
    LocalTransformX,
    LocalTransformY,
    LocalTransformZ,
    LocalRotationX,
    LocalRotationY,
    LocalRotationZ
}