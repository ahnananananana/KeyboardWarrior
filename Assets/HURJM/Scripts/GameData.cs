using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public int Level = 1;
    public int m_CurScene = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
