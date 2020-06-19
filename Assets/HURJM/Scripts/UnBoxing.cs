using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBoxing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
        }
    }

    void Test()
    {
        Debug.Log("Start");
        float old = Time.realtimeSinceStartup;
        int max = 10000;
        for (int i = 0; i < max; ++i)
        {
            for (int j = 0; j < max; ++j)
            {
                int a = 100;
                object b = a;
            }
        }
        float cur = Time.realtimeSinceStartup;
        Debug.Log(cur - old);

    }
}
