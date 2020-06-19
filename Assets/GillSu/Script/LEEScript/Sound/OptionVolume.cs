using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionVolume : MonoBehaviour
{
    public GameObject obj = null;
    // [SerializeField]
    // private Canvas canvas = null;
    public Canvas canvas = null;

   public void clickButton()
    { // enabled = 현재 캔버스의 켜져있는지의 상태 체크.
        if (canvas.enabled == false)
        {
            canvas.enabled = !canvas.enabled; // 현재 상태의 반대상태로 만든다.
                                              //obj.SetActive(!obj.activeSelf);
        }
    }
    public void Clickfalse()
    {
        if (canvas.enabled == true)
        {
            canvas.enabled = !canvas.enabled;
        }
    }

    void Start()
    {
        canvas.enabled = false;
        //clickButton();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
