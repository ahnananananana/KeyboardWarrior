using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hSpawnPoint : MonoBehaviour
{
    private List<Character> m_MonsterToSpawnList = new List<Character>();
    [SerializeField]
    private SphereCollider m_Collider;

    public void AddSpawnMonster(Character inMonster)
    {
        m_MonsterToSpawnList.Add(inMonster);
    }

    public void Spawn(int inNum, List<Character> inMonsterList)
    {
        for (int i = 0; i < inNum; ++i)
        {
            float x_offset = Random.Range(-m_Collider.radius, m_Collider.radius);
            float z_offset = Random.Range(-m_Collider.radius, m_Collider.radius);
            Vector3 pos = m_Collider.bounds.center;
            pos.x += x_offset;
            pos.z += z_offset;
            int index = Random.Range(0, m_MonsterToSpawnList.Count);
            Character monsterPrefab = m_MonsterToSpawnList[index];
            Character newMonster = Instantiate(monsterPrefab, pos, monsterPrefab.transform.localRotation).GetComponent<Character>();
            inMonsterList.Add(newMonster);
        }
    }
}
