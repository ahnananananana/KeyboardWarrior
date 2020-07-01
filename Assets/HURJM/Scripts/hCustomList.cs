using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelT<T>(T inVal);
public delegate void DelTInt<T>(int inIndex, T inVal);
public delegate void DelTList<T>(hCustomList<T> inList);

public class hCustomList<T> : List<T>
{
    private DelT<T> m_AddEvent;
    private DelT<T> m_RemoveEvent;
    private DelT<T> m_ClearEvent;
    private DelTList<T> m_ChangeEvent;
    private DelTInt<T> m_InsertEvent;

    public DelT<T> addEvent 
    {
        get => m_AddEvent;
        set
        {
            m_AddEvent += value;
        }
    }

    public DelT<T> removeEvent 
    {
        get => m_RemoveEvent;
        set
        {
            m_RemoveEvent += value;
        }
    }

    public DelT<T> clearEvent 
    {
        get => m_ClearEvent;
        set
        {
            m_ClearEvent += value;
        }
    }

    public DelTList<T> changeEvent 
    {
        get => m_ChangeEvent;
        set
        {
            m_ChangeEvent += value;
        }
    }

    public DelTInt<T> InsertEvent { get => m_InsertEvent; set => m_InsertEvent += value; }

    public new void Add(T inItem)
    {
        base.Add(inItem);
        m_AddEvent?.Invoke(inItem);
        m_ChangeEvent?.Invoke(this);
    }

    public new void Remove(T inItem)
    {
        base.Remove(inItem);
        m_RemoveEvent?.Invoke(inItem);
        m_ChangeEvent?.Invoke(this);
    }

    public new void RemoveAt(int index)
    {
        m_RemoveEvent?.Invoke(this[index]);
        base.RemoveAt(index);
        m_ChangeEvent?.Invoke(this);
    }

    public new void Insert(int inIndex, T inItem)
    {
        base.Insert(inIndex, inItem);
        m_InsertEvent(inIndex, inItem);
        m_ChangeEvent?.Invoke(this);
    }

    public new void Clear()
    {
        for (int i = 0; i < Count; ++i)
        {
            m_ClearEvent?.Invoke(this[i]);
            m_RemoveEvent?.Invoke(this[i]);
        }
        base.Clear();
        m_ChangeEvent?.Invoke(this);
    }

}
