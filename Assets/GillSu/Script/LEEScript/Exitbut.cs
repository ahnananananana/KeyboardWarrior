using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Exitbut : MonoBehaviour
{
    public void ExitButton()
    {
        Debug.Log("dd");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void OptionClick()
    {
        OptionVolume ov = FindObjectOfType<OptionVolume>();
        ov.clickButton();
    }
}
