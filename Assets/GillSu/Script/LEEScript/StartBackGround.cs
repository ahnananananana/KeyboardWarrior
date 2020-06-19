using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackGround : MonoBehaviour
{
    Vector2 invec = new Vector2(Screen.width, Screen.height);

    private void Awake()
    {
        transform.GetComponent<RectTransform>().sizeDelta = invec;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
