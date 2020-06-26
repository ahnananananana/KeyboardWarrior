using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Player player;
    public Monster monster;
    public MainData mainData;

    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("player exp ==" + player.m_EXP.m_CurrExp);
        Debug.Log("player lev ==" + player.m_EXP.m_Level);
        Debug.Log("player atk == " + player.m_Attack.m_CurrentValue);
        Debug.Log("player base atk == " + player.m_Attack.m_BaseValue);
        Debug.Log("monster max hp == " + monster.m_MaxHP.m_CurrentValue);
        Debug.Log("monster curr hp == " + monster.m_CurrHP);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainData.BuffData[0].ApplyBuff(player);
            Debug.Log("eee");
            Debug.Log("player exp ==" + player.m_EXP.m_CurrExp);
            Debug.Log("player lev ==" + player.m_EXP.m_Level);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            
            mainData.BuffData[0].ApplyBuff(player);
            Debug.Log("player atk == " + player.m_Attack.m_CurrentValue);
            Debug.Log("player exp == " + player.m_EXP.m_CurrExp);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            player.DealDamage(monster);
            Debug.Log("player exp ==" + player.m_EXP.m_CurrExp);
            Debug.Log("player total exp == " + player.m_EXP.m_TotalExp);
            Debug.Log("player lev ==" + player.m_EXP.m_Level);
            Debug.Log("monster exp == " + monster.EXP);
        }


    }
}
