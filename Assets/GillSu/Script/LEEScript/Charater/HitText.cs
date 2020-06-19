using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitText : MonoBehaviour
{
    public GameObject T_hit = null;
    Camera _uicamera = null;
    public Camera m_UICamera
    {
        get
        {
            if (_uicamera == null)
            {
                _uicamera = Camera.allCameras[1];
            }
            return _uicamera;
        }
    }

    Camera _maincamera = null;
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
    public float ModifyHeight = 1f;


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public void SetPosition(Transform target, float height)
    {
        this.transform.SetParent(GameObject.Find("Canvas").transform);
        this.transform.localScale = Vector3.one;
        ModifyHeight = height;
        StartCoroutine(FollowPosition(target));
    }

    IEnumerator FollowPosition(Transform target)
    {
        while (target != null)
        {
            Vector3 pos = m_mainCamera.WorldToScreenPoint(target.position);
            Vector3 temp = m_UICamera.ScreenToWorldPoint(pos);
            temp.y += ModifyHeight;

            GetComponent<RectTransform>().position = temp;
            yield return new WaitForFixedUpdate();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * Time.deltaTime * 2f);
    }
}
