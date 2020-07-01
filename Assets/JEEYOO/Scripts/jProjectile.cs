using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jProjectile : MonoBehaviour
{
    private Transform m_Transform;
    public Transform playerTransform;
    private float temptime = 0;
    private Vector3 target;
    private Vector3 targetVector;
    private float projectileSpeed = 8f;
    public Monster firebomb;
    public hAudioManager audiomanager;
    public AudioClip audio;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        audiomanager.Init(gameObject);
        

        target = playerTransform.position;
        targetVector = target - m_Transform.position;
        targetVector.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        temptime += Time.deltaTime;
        //transform.position = Vector3.MoveTowards(m_Transform.position, target, 10f * Time.deltaTime);
        transform.position += targetVector * Time.deltaTime * projectileSpeed;
        if (temptime >= 4.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crash");
        if (other.gameObject.tag == "Player")
        {
            Character scp = other.gameObject.GetComponent<Player>();
            Debug.Log(firebomb.m_Attack.m_CurrentValue);
            firebomb.DealDamage(other.gameObject.GetComponent<Player>());
            audiomanager.PlayClip(audio);
            Destroy(this.gameObject);
        }
    }
}
