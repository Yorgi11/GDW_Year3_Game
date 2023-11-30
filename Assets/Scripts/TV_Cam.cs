using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Cam : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    [SerializeField] private Transform target;
    void Update()
    {
        transform.forward = Vector3.Slerp(transform.forward, (target.position - transform.position).normalized, rotSpeed * Time.deltaTime);
    }
}
