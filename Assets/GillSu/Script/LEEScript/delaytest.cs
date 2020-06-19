using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delaytest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
        float old = Time.realtimeSinceStartup;
        int max = 100;
        for (int n = 0; n < max; ++n)
        {
            for (int m = 0; m < max; ++m)
            {
                int a = 100;
                //  int b = a;
                //   int c = b;
                object o = a;
                //int c = 0;
            }
        }
        float cur = Time.realtimeSinceStartup;
        Debug.Log("Using : " + (cur - old));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
