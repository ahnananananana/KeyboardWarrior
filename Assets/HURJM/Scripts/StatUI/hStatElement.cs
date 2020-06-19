using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hStatElement : MonoBehaviour
{
    [SerializeField]
    private int m_Id;
    [SerializeField]
    private TMPro.TMP_Text m_StatName;
    [SerializeField]
    private TMPro.TMP_Text m_StatValue;

    public int id { get => m_Id; set => m_Id = value; }

    public void Set(string inName, string inValue)
    {
        m_StatName.text = inName;
        m_StatValue.text = inValue;
    }
}
