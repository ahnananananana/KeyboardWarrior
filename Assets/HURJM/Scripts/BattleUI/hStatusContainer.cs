using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStatusContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject m_StatusSlotPrefab;

    public void AddStatus()//상태나 버프, 디버프 정보를 인수로 받는다
    {
        GameObject obj = Instantiate(m_StatusSlotPrefab, transform);
        hStatusSlot statusSlot = obj.GetComponent<hStatusSlot>();
        //받은 정보를 statusSlot에 넣는다. statusSlot.Init()
    }

    public void RemoveStatus()
    {
        hStatusSlot[] statusSlots = transform.GetComponentsInChildren<hStatusSlot>();
        for (int i = 0; i < statusSlots.Length; ++i)
        {
            //if(statusSlots[i] ==)
        }
    }
}
