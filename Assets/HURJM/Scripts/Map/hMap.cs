using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hMap : MonoBehaviour
{
    [SerializeField]
    private MapType m_Type;
    [SerializeField]
    private hEntrance[] m_EntranceList;
    [SerializeField]
    private Transform m_Lights;
    [SerializeField]
    private Light[] m_LightList;
    [SerializeField]
    private Transform m_SpawnPoints;
    private hSpawnPoint[] m_SpawnPointList;
    private List<Character> m_MonsterList;

    public event DelEntrance enterEvent;

    [SerializeField]
    private int m_Id;
    public int id { get => m_Id; }
    public MapType type { get => m_Type; set => m_Type = value; }
    public hEntrance[] entranceList { get => m_EntranceList; }

    private void Awake()
    {
        m_MonsterList = new List<Character>();

        for (int i = 0; i < m_EntranceList.Length; ++i)
        {
            m_EntranceList[i].enterEvent += EnterEntrance;
        }

        //m_LightList = m_Lights.GetComponentsInChildren<Light>();
        //SetType(m_Type);
        SetType(hLevelManager.current.mapType);

        m_SpawnPointList = m_SpawnPoints.GetComponentsInChildren<hSpawnPoint>();
    }

    public void StartMap()
    {
        for (int i = 0; i < m_SpawnPointList.Length; ++i)
            m_SpawnPointList[i].Spawn(1 * hLevelManager.current.curLevel, m_MonsterList);
    }

    private void Update()
    {
        if(IsEnd())
            OpenTheDoor();
    }

    private void OpenTheDoor()
    {
        for (int i = 0; i < m_EntranceList.Length; ++i)
            m_EntranceList[i].Open();
    }

    private bool IsEnd()
    {
        for (int i = 0; i < m_MonsterList.Count; ++i)
            if (m_MonsterList[i].m_state != Character.STATE.DEAD)
                return false;

        return true;
    }

    public void SetType(MapType inType)
    {
        m_Type = inType;
        Color lightColor = Color.white;
        switch (m_Type)
        {
            case MapType.NORMAL:
                lightColor = Color.white;
                break;
            case MapType.ELITE:
                lightColor = Color.cyan;
                break;
            case MapType.BOSS:
                lightColor = Color.red;
                break;
            case MapType.BONUS:
                lightColor = Color.yellow;
                break;
        }
        for (int i = 0; i < m_LightList.Length; ++i)
        {
            m_LightList[i].color = lightColor;
        }
    }

    public void SetSpawnMonster(Character[] inMonsterList)
    {
        for (int i = 0; i < inMonsterList.Length; ++i)
            for (int j = 0; j < m_SpawnPointList.Length; ++j)
                m_SpawnPointList[j].AddSpawnMonster(inMonsterList[i]);
    }

    private void EnterEntrance(hEntrance inEntrance) => enterEvent?.Invoke(inEntrance);

    public void Clear()
    {
        m_MonsterList.Clear();
    }

    public void ClearEvent() => enterEvent = null;
}
