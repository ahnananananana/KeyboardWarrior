using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hStatBar : MonoBehaviour
{
    [SerializeField]
    private Slider m_Slider;
    [SerializeField]
    private TMPro.TMP_Text m_Text;

    public void SetValue(float inVal)//현재 체력이 바뀔 때 실행
    {
        m_Slider.value = inVal;
        m_Text.text = (int)inVal + " / " + (int)m_Slider.maxValue;
    }
    public void SetMax(float inMax)//최대체력이 바뀔 때 실행
    {
        m_Slider.maxValue = inMax;
        m_Text.text = (int)m_Slider.value + " / " + (int)inMax;
    }
}
