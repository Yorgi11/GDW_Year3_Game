using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysArmCtrl : MonoBehaviour
{
    [SerializeField] Transform shoulder;
    [SerializeField] Transform elbow;
    [SerializeField] private Transform shoulderTarget;
    [SerializeField] private Transform elbowTarget;
    [SerializeField] private float moveRate;

    [SerializeField] private Rigidbody srb, erb;
    void Update()
    {
        shoulder.localRotation = Quaternion.Slerp(shoulder.localRotation, shoulderTarget.localRotation, moveRate * Time.deltaTime);
        elbow.localRotation = Quaternion.Slerp(elbow.localRotation, elbowTarget.localRotation, moveRate * Time.deltaTime);
        shoulder.position = shoulderTarget.position;
    }
}
