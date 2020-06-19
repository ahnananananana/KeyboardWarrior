using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleton<T> : MonoBehaviour where T : MonoBehaviour // T는 MonoBehaviour의 파생클래스로 한정 지어줌.
{
    private static T instance = null;
    public static T I
    {
        get
        {
            if (null == instance)
            {
                instance = FindObjectOfType<T>(); // 씬 내의 오브책트를 찾아 넣어줌
                if (null == instance) // 찾아봐도 존재하지않으면
                {
                    GameObject obj = new GameObject(); // 새로운 게임 오브젝트 생성
                    obj.name = typeof(T).ToString(); // 이름은 스크립트 자체의 이름을 넣어줌
                    instance = obj.AddComponent<T>(); // 해당 스크립트를 컴포넌트로 포함시켜줌.
                }
            }
            return instance;
        }
    }
}
