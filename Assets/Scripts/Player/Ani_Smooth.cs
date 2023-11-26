using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ani_Smooth : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve posCurve;
    [SerializeField] private AnimationCurve rotCurve;
    [SerializeField] private Transform[] animated;
    [SerializeField] private Transform[] interpolated;
    void Update()
    {
        for (int i = 0; i < animated.Length; i++)
        {
            interpolated[i].localRotation = Quaternion.Slerp(interpolated[i].localRotation, animated[i].localRotation, rotCurve.Evaluate(speed * Time.deltaTime));
            interpolated[i].localPosition = Vector3.Lerp(interpolated[i].localPosition, animated[i].localPosition, posCurve.Evaluate(speed * Time.deltaTime));
        }
    }
}
