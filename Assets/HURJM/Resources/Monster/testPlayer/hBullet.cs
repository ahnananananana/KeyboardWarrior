using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBullet : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    private Character m_Shooter;
    [SerializeField]
    private LayerMask m_EnemyLayer;

    public Character shooter { get => m_Shooter; set => m_Shooter = value; }

    public void Start()
    {
        StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
        float acc = 0f;
        while(acc < 5f)
        {
            Vector3 pos = transform.position;
            pos += transform.forward * m_Speed * Time.fixedDeltaTime;
            transform.position = pos;
            acc += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        DestroyImmediate(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer) == m_EnemyLayer.value)
        {
            Debug.Log("Collision!");
            Character target = other.GetComponent<Character>();
            m_Shooter.DealDamage(target);
            Destroy(gameObject);
        }
    }


}
