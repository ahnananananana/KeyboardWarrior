using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jProjectile : MonoBehaviour
{
    private Transform m_Transform;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float temptime = 0;
        temptime += Time.deltaTime;
        //projectile.transform.position = this.gameObject.transform.position;
        transform.position = Vector3.MoveTowards(m_Transform.position, playerTransform.position, 10f * Time.deltaTime);
        if (temptime >= 2.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
