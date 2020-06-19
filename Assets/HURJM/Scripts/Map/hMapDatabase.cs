using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class hMapDatabase : ScriptableObject
{
    [SerializeField]
    private hMap[] m_MapList;

    public hMap[] mapList { get => m_MapList; }

    public hMap GetMap(int inId)
    {
        for (int i = 0; i < m_MapList.Length; ++i)
        {
            hMap map = m_MapList[i];
            if (map.id == inId)
            {
                return map;
            }
        }

        return default;
    }
}
