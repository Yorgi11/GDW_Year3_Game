using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Transform target;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        if (target != null) transform.forward = Vector3.Lerp(transform.forward, (target.position - transform.position).normalized, 8f * Time.deltaTime);
    }
    public void SliderBar(float maxValue, float currentValue)
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }
}
