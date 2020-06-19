using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//public delegate void DelVoid();

public class StartUbtton : MonoBehaviour
{
    public enum STATE
    {
        CREATE,MOVE,STOP,RETURN, CHANGE
    }
    STATE m_State = STATE.CREATE;
    public MouseEvent m_mouseEvent;
    // Start is called before the first frame update
    void Start()
    {
        m_State = STATE.CREATE;
        //m_mouseEvent = new MouseEvent();
        m_mouseEvent.m_MouseOverEvent += OnOver;
        m_mouseEvent.m_MouseExitEvent += MouseOut;
        m_mouseEvent.m_MouseClickEvent += OnClick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MouseOut(PointerEventData eventData)
    {
        if (m_State == STATE.STOP)
        {
            ChangeState(STATE.RETURN);
        }

    }
    void OnOver(PointerEventData eventData)
    {
        if (m_State == STATE.CREATE)
        {
            ChangeState(STATE.MOVE);
        }
    }
    void OnClick(PointerEventData eventData)
    {
        if (m_State == STATE.STOP)
        {
            ChangeState(STATE.CHANGE);
        }
    }
    public Vector2 vec = new Vector2(1, 1);
   public void ChangeState(STATE s)
        {
            if (m_State == s) return;
            m_State = s;
            switch (m_State)
            {
                case STATE.CREATE:
                    break;
                case STATE.MOVE:
                StartCoroutine(SizeChange(STATE.STOP, 1.3f, 10f));
                ChangeState(STATE.STOP);
                    break;
                case STATE.STOP:
                    break;
            case STATE.RETURN:
                StartCoroutine(SizeChange(STATE.STOP, 1f, 10f));

                ChangeState(STATE.CREATE);
                break;
            case STATE.CHANGE:
          
                switch (Gilsu.GameData.I.m_CurScene)
                {
                    case 0:
                        SceneLoad.I.SceneChange(2);
                        break;
                    case 1:
         
                        break;
                    case 2:
                        SceneLoad.I.SceneChange(0);
                        break;
                }
                break;
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

    public void Exitbutt()
    {
        Application.Quit();
    }
}


