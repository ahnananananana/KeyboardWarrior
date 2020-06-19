using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환

public class SceneChange : MonoBehaviour
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
     
        }
    }
}
