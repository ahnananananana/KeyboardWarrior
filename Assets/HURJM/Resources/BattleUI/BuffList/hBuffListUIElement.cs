using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hBuffListUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Buff m_Buff;
    [SerializeField]
    private Image m_Image;
    [SerializeField]
    private GameObject m_Description;
    [SerializeField]
    private TMPro.TMP_Text m_DescriptionText;

    public Buff buff { get => m_Buff; }

    private void Awake() => m_Description.SetActive(false);
    public void Set(Buff inBuff)
    {
        m_Image.sprite = inBuff.icon;
        m_DescriptionText.text = inBuff.buffName;
    }

    public void OnPointerEnter(PointerEventData eventData) => m_Description.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => m_Description.SetActive(false);
}
