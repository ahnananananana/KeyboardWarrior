using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hStatusSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TMPro.TMP_Text m_ToolTip;

    public void Init()//상태나 버프, 디버프 정보를 인수로 받는다
    {
        m_ToolTip.gameObject.SetActive(true);
        //받은 정보를 초기화 한다.
        //툴팁내용 초기화
    }

    private void Awake()
    {
        m_ToolTip.gameObject.SetActive(false);
    }

    //툴팁 상호작용
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_ToolTip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_ToolTip.gameObject.SetActive(false);
    }
}
