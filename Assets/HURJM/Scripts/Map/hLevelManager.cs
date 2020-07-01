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
                GameObject gob = Instantiate(Resources.Load(prefabPath)) as GameObject;
                gob.name = "LevelManager";
                _Instance = gob.GetComponent<hLevelManager>();
                DontDestroyOnLoad(gob);
            }

            return _Instance;
        }
    }

    [SerializeField]
    private hLevelData[] m_NormalLevelDataList;
    [SerializeField]
    private hLevelData[] m_BossLevelDataList;
    [SerializeField]
    private int m_CurLevel = 1;

    private MapType m_NextMapType;

    public int curLevel { get => m_CurLevel; set => m_CurLevel = value; }
    public MapType nextMapType { get => m_NextMapType; set => m_NextMapType = value; }

    public hCustomList<Buff> buffList;

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

        buffList = new hCustomList<Buff>();
    }

    public hLevelData LoadNextLevel()
    {
        switch(m_NextMapType)
        {
            case MapType.BOSS:
                return m_BossLevelDataList[0];
            default:
                return m_NormalLevelDataList[0];
        }
    }

    public void GameOver()
    {
        m_CurLevel = 1;
        SceneLoad.I.SceneChange(0);
    }
}
