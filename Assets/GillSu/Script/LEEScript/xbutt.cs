using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class xbutt : MonoBehaviour
{
    public MouseEvent m_mouseev = null;
    public GameObject obj = null;
   
   
    void XCheck(PointerEventData eventData)
    {
        Destroy(obj);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_mouseev.m_MouseClickEvent += XCheck;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
