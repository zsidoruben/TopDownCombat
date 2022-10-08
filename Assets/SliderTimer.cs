using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class SliderTimer : MonoBehaviour
{
    Slider slider;
    bool completed = false;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void Reset()
    {
        completed = true;
        slider.value = 0;
    }
    public void StartLoading(float time)
    {
        slider.value = 0;
        completed = false;
        slider.maxValue = time;
    }
    private void Update()
    {
        if (completed)
        {
            return;
        }
        if (slider.value != slider.maxValue)
        {
            slider.value += Time.deltaTime;
        }
        else
        {
            Reset();
        }
    }
}
