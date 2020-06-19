using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hStatBar : MonoBehaviour
{
    [SerializeField]
    private Slider m_Slider;
    private float m_Max;

    public void SetValue(float inVal) => m_Slider.value = inVal;//현재 체력이 바뀔 때 실행
    public void SetMax(float inMax)//최대체력이 바뀔 때 실행
    {
        m_Slider.maxValue = inMax;
    }
}
