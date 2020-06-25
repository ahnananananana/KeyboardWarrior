using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    protected int m_MonsterEXP = 100;

    public int EXP { get => m_MonsterEXP;}

    public SkinnedMeshRenderer[] renderers;// 추가한거(대한)

    // Start is called before the first frame update
    new void Start()
    {
        renderers = this.GetComponentsInChildren<SkinnedMeshRenderer>(); // 아웃라인 (대한)
        InitStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitStats()
    {
        m_MaxHP.m_BaseValue = 100;
        m_MaxMP.m_BaseValue = 0;

        m_CurrHP = m_MaxHP.m_CurrentValue;
        m_CurrMP = m_MaxMP.m_CurrentValue;

        m_Attack.m_BaseValue = 60;
        m_Defense.m_BaseValue = 20;

        m_Magic.m_BaseValue = 60;
        m_Resistance.m_BaseValue = 20;

        m_MoveSpeed.m_BaseValue = 20;
        m_AttackSpeed.m_BaseValue = 20;
    }

    private void OnMouseEnter() // 마우스 갖다댔을 때 아웃라인 생성(대한)
    {
        if (!Input.GetMouseButton(0))
        {
            for (int i = 0; i < renderers.Length; ++i)
            {
                SkinnedMeshRenderer renderer = renderers[i];
                renderer.material.SetFloat("_OutlineWidth", 0.06f);
                renderer.material.SetColor("_OutlineColor", new Color(1f, 0f, 0f));
            }
        }
    }

    private void OnMouseExit() // 마우스 뗐을 때(대한)
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(CheckMouseButton0());
        }
        else
        {
            SetNullOutline();
        }
    }

    public void SetNullOutline() // 많이 쓰일것같아서 만든 아웃라인 없애는 함수(대한)
    {
        for (int i = 0; i < renderers.Length; ++i)
        {
            SkinnedMeshRenderer renderer = renderers[i];
            renderer.material.SetFloat("_OutlineWidth", 0.0f);
            renderer.material.SetColor("_OutlineColor", new Color(0f, 0f, 0f));
        }
    }

    IEnumerator CheckMouseButton0() // 타겟팅상태로 마우스를 드래그 했을 때 실행(대한)
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SetNullOutline();
                break;
            }
            yield return null;
        }
    }
}
