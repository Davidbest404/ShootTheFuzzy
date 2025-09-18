using UnityEngine;
using System.Linq;

public class AnimationGroupInstance
{
    private readonly Transform target;
    private readonly AnimationInstance[] instance;
    private readonly Vector3 originalPosition;
    private readonly Vector3 originalRotation;

    public AnimationGroupInstance(Transform target, ObjectProceduralAnimationGroup animation)
    {
        this.target = target;
        originalPosition = target.localPosition;
        originalRotation = target.eulerAngles;
        instance = animation.Animations
            .Select(a => new AnimationInstance(a))
            .ToArray();
    }

    public void Update(float t)
    {
        var currentOffset = Vector3.zero;
        var currentRotation = Vector3.zero;

        foreach (var animationInstance in instance)
        {
            animationInstance.Update(t, ref currentOffset, ref currentRotation);
        }

        target.localPosition = originalPosition + currentOffset;
        target.localRotation = Quaternion.Euler(originalRotation + currentRotation);
    }
}
