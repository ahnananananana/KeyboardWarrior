using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingImage : MonoBehaviour
{
    public enum STATE
    {
        CREATE, CHANGE, END
    }
    STATE m_State = STATE.CREATE;

    public Sprite[] LoadSprite = new Sprite[4];
    Vector2 Myvector = new Vector2(Screen.width, Screen.height);
    //public Sprite LevelImage;
    int RandNumber = 0; // 이미지 스프라이이트 넘버.
    int num;
    private void Awake()
    {
//Debug.Log(Screen.height);
        //transform.gameObject.GetComponent<RectTransform>().sizeDelta = Myvector;
        transform.GetComponent<RectTransform>().sizeDelta = Myvector;
       
    }
    void Start()
    {

       StartCoroutine(imageChange(0.3f));
    }


    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator imageChange(float i)
    {
        RandNumber = Random.Range(0, 3);
        gameObject.GetComponent<Image>().sprite = LoadSprite[RandNumber];
        yield return new WaitForSeconds(i);
        RandNumber = Random.Range(0, 3);
        num = RandNumber;
        if (num == RandNumber)
        {
            RandNumber += 1;
            if (RandNumber == 3)
            {
                RandNumber = 0;
                gameObject.GetComponent<Image>().sprite = LoadSprite[RandNumber];
            }
        gameObject.GetComponent<Image>().sprite = LoadSprite[RandNumber];

        }
     


    }
}
