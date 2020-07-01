using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hClearPopup : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text m_Text;

    public void Set(int inLevel)
    {
        m_Text.text = inLevel + " 스테이지 클리어";
        StartCoroutine(DelayEnd());
    }

    private IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
