using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object / Monster Data", order = int.MaxValue)]
public class MonsterData : ScriptableObject // 씬에있는 오브젝트에 붙힐 수 없음 붙히려면 Monobehevior을 상속받아야함
{

    public string monsterName;
    public int HP;
    public float MoveSpeed;
}
