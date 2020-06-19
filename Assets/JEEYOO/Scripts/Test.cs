using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Player player;
    public Player player2;
    public Monster monster;
    public MainData mainData;

    

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("player atk ==" + player.m_Attack.m_CurrentValue);
        Debug.Log("player2 atk ==" + player.m_Attack.m_CurrentValue);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainData.ItemData[0].Equip(player);
            Debug.Log(player.m_Attack.m_CurrentValue);
            Debug.Log(monster.m_CurrHP);
            Debug.Log(player.m_EXP.m_Level);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            
            mainData.BuffData[0].ApplyBuff(player);
            Debug.Log("player atk == " + player.m_Attack.m_CurrentValue);
            Debug.Log("player2 atk == " + player.m_Attack.m_CurrentValue);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            mainData.BuffData[0].ApplyBuff(monster);
            Debug.Log("player atk == " + player.m_Attack.m_CurrentValue);
            Debug.Log("monster atk == " + monster.m_Attack.m_CurrentValue);
        }


    }
}
