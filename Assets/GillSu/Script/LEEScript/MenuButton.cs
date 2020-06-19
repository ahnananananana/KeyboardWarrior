using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class MenuButton : MonoBehaviour
{
    public MouseEvent m_mouse = null;
    public GameObject m_image = null;
    public Transform m_cavas;
    Vector2 rvec = new Vector2(1, 1);
    public enum STATE
    {
        CREATE, MOVE, STOP, RETURN, OPEN
    }
    STATE m_State = STATE.CREATE;
    void MouseOut(PointerEventData eventData)
    {
        if (m_State == STATE.STOP)
        {
            ChangeState(STATE.RETURN);
        }

    }
    void OnOver(PointerEventData eventData)
    {
        if (m_State == STATE.CREATE || m_State == STATE.STOP)
        {
            ChangeState(STATE.MOVE);
        }
    }
    void OnClick(PointerEventData eventData)
    {
        if (m_State == STATE.STOP)
        {
            ChangeState(STATE.OPEN);
        }
    }
    public void ChangeState(STATE s)
    {
        if (m_State == s) return;
        m_State = s;
        switch (m_State)
        {
            case STATE.CREATE:
                break;
            case STATE.MOVE:
                
                StartCoroutine(SizeChange(STATE.STOP, 2f, 10f));
                ChangeState(STATE.STOP);
                break;
            case STATE.STOP:
                break;
            case STATE.RETURN:
                StartCoroutine(SizeChange(STATE.STOP, 1, 10f));
                ChangeState(STATE.STOP);
                break;
            case STATE.OPEN:
                EscBar();
                ChangeState(STATE.RETURN);
                break;  
        }
    
    }
    void escbutton()
    {
        if (GameObject.Find("UITap") == null)
        {
            EscBar();
        }
        else
        {
            Destroy(GameObject.Find("UITap"));
        }
    }
    public void EscBar()
    {
   
            GameObject m_ims = Instantiate(m_image);
            m_ims.name = "UITap";
            m_ims.transform.SetParent(m_cavas);
            m_ims.transform.localPosition = Vector2.zero;
            m_ims.transform.localScale = new Vector2(2.5f, 2.5f);
        
    }
    // Start is called before the first frame update
    void Start()
    {
      
        m_State = STATE.CREATE;
        m_mouse.m_MouseOverEvent += OnOver;
        m_mouse.m_MouseClickEvent += OnClick;
        m_mouse.m_MouseExitEvent += MouseOut;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escbutton();
        }
    }

    IEnumerator SizeChange(STATE s, float target, float speed)
    {
        Vector2 scale = GetComponent<RectTransform>().localScale;
        while (Mathf.Abs(scale.x - target) > 0.1f)
        {
            scale = Vector2.Lerp(scale, new Vector2(target, target), Time.smoothDeltaTime * speed);
            GetComponent<RectTransform>().localScale = scale;
            yield return null;
        }
        GetComponent<RectTransform>().localScale = new Vector2(target, target);
        ChangeState(s);
    }
    public void ExitClick()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
