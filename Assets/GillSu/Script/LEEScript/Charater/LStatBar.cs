using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LStatBar : MonoBehaviour
{
    public Image m_Fillimage;
    public Image m_BGimage;
    Camera m_Uicamera = null;
    public Camera m_UICamera
    {
        get
        {
            if (m_Uicamera == null)
            {
                m_Uicamera = Camera.allCameras[1];
            }
            return m_Uicamera;
        }
        set
        {
            m_Uicamera = value;
        }
    }
    Camera _maincamera = null;
    public Camera GetCamera()//m_mainCamera;
    {

        if (_maincamera == null)
        {
            _maincamera = Camera.main;
        }
        return _maincamera;
    }
    public Camera m_mainCamera
    {
        get
        {
            if (_maincamera == null)
            {
                _maincamera = Camera.main;
            }
            return _maincamera;
        }
    }
    public Slider m_SilderBar = null;
    public float ModefyHeight = 1f;

    public void SetPosition(Transform target,/* float height,*/ Color fill, Color bg)
    {
        /*this.transform.SetParent(GameObject.Find("Canvas").transform);
        //해당 오브젝트를 자식으로 상속한다 -  canvas의 위치로.
        this.transform.localScale = Vector3.one;
        // 로컬 스케일 1로 설정.*/
        SetColor(fill, bg);
       // ModefyHeight = height;
        StartCoroutine(FollowPosition(target));
    }
    public void SetColor(Color fill, Color bg)
    {
        m_Fillimage.color = fill;
        m_BGimage.color = bg;

    }
    IEnumerator FollowPosition(Transform target)
    {
        while (target != null)
        {
            Vector3 pos = m_mainCamera.WorldToScreenPoint(target.position);
            // 화면상의 포인트를 월드로 이동해주고
            Vector3 temp = m_UICamera.ScreenToWorldPoint(pos);
            // 월드상 좌표를 화면 스크린으로 다시 옮겨준다.
            temp.y += ModefyHeight;
            GetComponent<RectTransform>().position = temp;
            yield return new WaitForFixedUpdate();
            //WaitForFixedUpdate = 고정 프레입으로 업데이트.(프로젝트 설정에서 초당 프레임 설정 가능.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SilderBar.value = 1f;
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
