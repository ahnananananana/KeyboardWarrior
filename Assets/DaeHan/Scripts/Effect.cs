using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public enum STATE
    {
        CREATE,SHOW,DELETE
    }
    public STATE state = STATE.CREATE;
    
    public float fDeleteEffectTime = 2.0f;
    public float fTime = 0.0f;

    private void Start()
    {
        ChangeState(STATE.SHOW);
    }

    void ChangeState(STATE s)
    {
        if (state == s) return;
        state = s;

        switch(s)
        {
            case STATE.CREATE:
                break;
            case STATE.SHOW:
                StartCoroutine(ShowEffect());
                break;
            case STATE.DELETE:
                Delete();
                break;
        }
    }

    public IEnumerator ShowEffect()
    {
        while (true)
        {
            fTime += Time.smoothDeltaTime;
            if (fTime >= fDeleteEffectTime)
            {
                ChangeState(STATE.DELETE);
            }
            yield return null;
        }
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
