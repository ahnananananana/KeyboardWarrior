﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hBuffPopup : MonoBehaviour
{
    [SerializeField]
    private Image m_Image;
    [SerializeField]
    private TMPro.TMP_Text m_Text;

    private void OnEnable()
    {
        StartCoroutine(DelayEnd());
    }

    public void Set(Buff inBuff)
    {
        m_Image.sprite = inBuff.icon;
        m_Text.text = inBuff.buffName;
    }

    private IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
