using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelEntrance(hEntrance inEntrance);

public class hEntrance : MonoBehaviour
{
    public event DelEntrance enterEvent;
    [SerializeField]
    private LayerMask m_PlayerLayer;
    [SerializeField]
    private MapType m_NextMapType;
    private Collider m_Collider;

    [SerializeField]
    private int m_Id;
    [SerializeField]
    private Light m_Light;

    public int id { get => m_Id; set => m_Id = value; }
    public MapType nextMapType { get => m_NextMapType; set => m_NextMapType = value; }

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = false;
        m_Light.enabled = false;
        SetType(m_NextMapType);
    }

    public void SetType(MapType inType)
    {
        m_NextMapType = inType;
        Color lightColor = Color.white;
        switch (m_NextMapType)
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
        m_Light.color = lightColor;
    }

    public void Open()
    {
        m_Collider.enabled = true;
        m_Light.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if((1  << other.gameObject.layer) == m_PlayerLayer)
        {
            enterEvent?.Invoke(this);
        }
    }

    private void OnMouseDown()
    {
        enterEvent?.Invoke(this);
    }


}
