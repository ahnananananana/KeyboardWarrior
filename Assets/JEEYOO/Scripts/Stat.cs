using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using UnityEngine.UIElements;

public delegate void DelVoid();
[Serializable]
public class Stat
{
    public event DelVoid changeEvent;
    //m_BaseValue는 스텟의 기본값. 이 값을 기반으로 모든 버프/아이템으로 인한 증감이 계산됨.
    public float m_BaseValue;

    //버프가 적용된 값을 받아올 때는 m_CurrentValue를 가져오면 됨(모든 버프가 계산 된 값을 복사해온 값)
    //CalculateValue는 버프를 합산하는 함수

    //isChanged는 스텟값이 변했을 때만 CalculateValue를 계산하도록(버프 적용 계산) 하기 위한 bool 값
    //즉, 스텟값이 변하지도 않았는데 버프 연산을 처음부터 다시 하면 성능이 저하될 것 같아서 만든 코드

    //m_LastBaseValue는 m_BaseValue(버프 적용 전, 기본 스텟 값)를 직접 건드리지 않기 위해 만든 변수(m_BaseValue를 담는다)
    public float m_CurrentValue
    {
        get
        {
            if (isChanged || m_BaseValue != m_LastBaseValue)
            {
                m_LastBaseValue = m_BaseValue;
                tempValue = CalculateFinalValue();
                isChanged = false;
                changeEvent?.Invoke();
            }
            return tempValue;
        }
    }

    private bool isChanged = true;
    private float tempValue;
    private float m_LastBaseValue = float.MinValue;

    //readonly는 const처럼 선언 이후에 값을 바꿀 수 없음
    //단, const와 다르게 초기화 할 때 뿐 아니라, 해당 객체의 생성자에서는 변경이 가능하며,
    //List의 경우 리스트 자체는 못 바꾸지만, 리스트 내의 요소는 추가/제거 가능
    private readonly List<StatModifier> L_statModifiers;

    //위의 리스트를 건드리지 않고 보여주기 위해 복사하는 리스트
    private readonly ReadOnlyCollection<StatModifier> L_showStatModifiers;

    //기본 생성자가 있어야 null reference 에러가 나지 않음
    public Stat()
    {
        L_statModifiers = new List<StatModifier>();
        L_showStatModifiers = L_statModifiers.AsReadOnly();
    }

    public Stat(float basevalue) : this()
    {
        m_BaseValue = basevalue;

    }

    public void AddModifier(StatModifier modifier)
    {
        isChanged = true;
        L_statModifiers.Add(modifier);
        //연산 순서를 위해(합연산, 곱연산의 구분) StatModifier의 order멤버변수에 따라 Sort하는 코드
        L_statModifiers.Sort(CompareModOrder);
    }

    private int CompareModOrder(StatModifier a, StatModifier b)
    {
        if (a.m_Order < b.m_Order) return -1;
        else if (a.m_Order > b.m_Order) return 1;
        return 0;
    }

    public bool RemoveModifier(StatModifier modifier)
    {
        //List에서 해당 요소가 제거되면 true를 반환, 아니면 false를 반환.
        if (L_statModifiers.Remove(modifier))
        {
            isChanged = true;
            return true;
        }
        return false;
    }

    //특정 아이템/버프로 인해 생긴 스텟의 증/감을 모두 해제하는 함수
    //for문의 i가 0에서 시작하지 않고 뒤(리스트의 카운트 - 1)에서 시작하는 이유는, 앞에서부터 지우면 리스트의 요소들이 앞으로 당겨지기 때문에
    //지워지지 않는 버프가 있을 수 있기 때문. 즉, 뒤에서부터 지워야 누락되는 요소가 생기지 않음
    public bool RemoveAllModFromSource(object source)
    {
        bool didRemove = false;
        for (int i = L_statModifiers.Count - 1; i >= 0; i--)
        {
            if (L_statModifiers[i].m_Source == source)
            {
                isChanged = true;
                didRemove = true;
                L_statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    private float CalculateFinalValue()
    {
        float finalValue = m_BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < L_statModifiers.Count; i++)
        {
            StatModifier mod = L_statModifiers[i];

            if (mod.m_Type == StatModType.Flat)
            {
                finalValue += mod.m_Value;
            }
            else if (mod.m_Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.m_Value;
                if (i + 1 >= L_statModifiers.Count || L_statModifiers[i + 1].m_Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }

            if (mod.m_Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.m_Value;
            }
        }

        
        return (float)Math.Round(finalValue, 2);
    }

    public void IncreaseBaseValue(float increase)
    {
        m_BaseValue *= increase;
    }
}
