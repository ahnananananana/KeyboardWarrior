using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public BGMsound bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm.GetComponent<BGMsound>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bgm.EffPlay();
        }
    }
}
