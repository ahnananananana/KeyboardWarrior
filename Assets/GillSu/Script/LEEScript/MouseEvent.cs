using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void DelPointerEventData(PointerEventData eventData);

public class MouseEvent : MonoBehaviour, IPointerClickHandler, // 인터페이스
    IDragHandler,
    IEndDragHandler,
    IPointerExitHandler,
    IPointerEnterHandler
{
    public event DelPointerEventData m_MouseOverEvent;
    public event DelPointerEventData m_MouseClickEvent;
    public event DelPointerEventData m_MousedragEvent;
    public event DelPointerEventData m_MousedragingEvent;
    public event DelPointerEventData m_MouseExitEvent;

    public void OnPointerClick(PointerEventData eventData) // 클릭 눌렀다 땠을시
    {
        if (m_MouseClickEvent != null) m_MouseClickEvent(eventData);
    }
    public void OnDrag(PointerEventData eventData) // 마우스 클릭 후 움직일시
    {
        // transform.position = Input.mousePosition;
        if (m_MousedragingEvent != null) m_MousedragingEvent(eventData);
    }
    public void OnEndDrag(PointerEventData eventData) //드레그를 끝냈을 시
    {
        if (m_MousedragEvent != null) m_MousedragEvent(eventData);
    }
    public void OnPointerEnter(PointerEventData eventData) // 마우스가 오버가 되었을 시.(마우스 포인터가 해당 영역에 진힙 시 호출)
    {
        if (m_MouseOverEvent != null) m_MouseOverEvent(eventData);
    }
    public void OnPointerExit(PointerEventData eventData) // OnPointerEnter 끝났을 시 호출
    {
        if (m_MouseExitEvent != null) m_MouseExitEvent(eventData);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
