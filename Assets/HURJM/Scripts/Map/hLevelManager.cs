using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hLevelManager : MonoBehaviour
{
    private static hLevelManager _Instance;

    private static string prefabPath = "Prefabs/LevelManager";

    public static hLevelManager current
    {
        get
        {
            if (_Instance == null) _Instance = FindObjectOfType<hLevelManager>();
            if (_Instance == null)
            {
                GameObject gob = Resources.Load(prefabPath) as GameObject;
                gob.name = "LevelManager";
                _Instance = gob.GetComponent<hLevelManager>();
                DontDestroyOnLoad(gob);
            }

            return _Instance;
        }
    }

    [SerializeField]
    private hLevelData[] m_LevelDataList;
    [SerializeField]
    private int m_CurLevel = 1;

    public MapType mapType;

    public int curLevel { get => m_CurLevel; set => m_CurLevel = value; }

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
    }

    public hLevelData LoadNextLevel()
    {
        return m_LevelDataList[0];
    }

    public void GameOver()
    {
        m_CurLevel = 1;
        SceneLoad.I.SceneChange(0);
    }
}
