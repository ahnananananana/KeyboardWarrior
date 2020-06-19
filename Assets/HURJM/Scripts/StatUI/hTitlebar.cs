using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hTitlebar : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Transform m_Window;

    private Vector3 m_PrePos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        m_PrePos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        Vector3 curPos = eventData.position;
        m_Window.position += curPos - m_PrePos;
        m_PrePos = curPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("drop");
    }
}
