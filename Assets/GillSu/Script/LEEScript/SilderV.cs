using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilderV : MonoBehaviour
{
    [SerializeField]
    private Slider m_Slider;
    // Start is called before the first frame update
    void Start()
    {
        SceneLoad.I.GetSliderValue();
        StartCoroutine(StartSlider());
    }

    private IEnumerator StartSlider()
    {
        m_Slider.value = 0f;
        float sliderVal = SceneLoad.I.GetSliderValue();
        while (sliderVal < 1f )
        {
            m_Slider.value = Mathf.MoveTowards(m_Slider.value, 1f, Time.deltaTime);
            yield return null;
            //sliderVal = SceneLoad.I.GetSliderValue();
            sliderVal = m_Slider.value;
        }
        m_Slider.value = 1f;
        SceneLoad.I.SceneActive();
    }
}
