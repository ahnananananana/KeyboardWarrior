using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    public GameObject uObj = null;
    public Transform m_canvas = null;
    public BGMsound bgm;
    // Start is called before the first frame update

    public void OpenUI()
    {
        GameObject obj = Instantiate(uObj);
        obj.name = "UITap";
        obj.transform.SetParent(m_canvas);
        obj.transform.localPosition = Vector2.zero;
        obj.transform.localScale = new Vector2(2.5f, 2.5f);
    }
    void Start()
    {
        bgm.GetComponent<BGMsound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bgm.EffPlay();
            if (GameObject.Find("UITap") != null)
            {
                Destroy(GameObject.Find("UITap"));
            }
            else {
                OpenUI();
            }
        }
  
       /* void attack()
        {
            //데미지
            //소리
            //BGMsound.I.EffPlay(attacksound);
        }*/
    }
}
