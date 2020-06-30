using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public LayerMask m_CastLayer;
    public LayerMask m_AlphaCastLayer;
    public Transform m_CameraTransform = null;
    public Transform m_FollowObject = null;
    public Vector3 m_OffSet = Vector3.zero;
    Vector3 m_Rotation;
    public float m_RotSpeed = 1800.0f;
    public float m_RotSmooth = 15.0f;
    public float m_ZoomSpeed = 300f;
    public float m_ZoomSmooth = 10f;
    public Vector2 RotationXRange = new Vector2(0,80);
    public Vector2 ZoomRange = new Vector2(1, 30);
    public float CollisionOffset = 1.0f;
    // Start is called before the first frame update    
    float m_Dist = 0f;
    float m_TargetDist = 0f;
    void Start()
    {
        m_TargetDist = m_Dist = Vector3.Distance(transform.position, m_FollowObject.position);
        m_Rotation = transform.rotation.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            m_Rotation.x -= Input.GetAxis("Mouse Y") * m_RotSpeed * Time.smoothDeltaTime;
            m_Rotation.y += Input.GetAxis("Mouse X") * m_RotSpeed * Time.smoothDeltaTime;
            m_Rotation.x = ModifyRot(m_Rotation.x, RotationXRange.x, RotationXRange.y);
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0 || m_TargetDist != m_Dist)
        {
            m_TargetDist -= Input.GetAxis("Mouse ScrollWheel") * m_ZoomSpeed * Time.smoothDeltaTime;
            m_TargetDist = Mathf.Clamp(m_TargetDist, ZoomRange.x, ZoomRange.y);
            m_Dist = Mathf.Lerp(m_Dist, m_TargetDist, Time.smoothDeltaTime * m_ZoomSmooth);
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(m_Rotation), Time.smoothDeltaTime * m_RotSmooth);
        
        Vector3 rotationOffset = transform.rotation * -Vector3.forward * m_Dist;
        Vector3 org = m_FollowObject.position + m_OffSet;

        Ray ray = new Ray();
        ray.origin = org;
        ray.direction = rotationOffset;
        ray.direction.Normalize();
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_Dist + CollisionOffset, m_CastLayer))
        {
            Vector3 point = hit.point - ray.direction * CollisionOffset;            
            transform.position = point;
            m_Dist = Vector3.Distance(transform.position, org);
        }
        else
        {
            transform.position = org + rotationOffset;
        }

        if (Physics.Raycast(ray, out hit, m_Dist + CollisionOffset, m_AlphaCastLayer))
        {
            Color col = hit.collider.gameObject.GetComponent<MeshRenderer>().materials[0].GetColor("_Color");
            col.a = 0f;
            hit.collider.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_Color",col);
        }
    }

    float ModifyRot(float angle, float min, float max)
    {
        if(angle > 360f)
        {
            angle -= 360f;
        }
        if(angle < -360f)
        {
            angle += 360f;
        }
        return angle = Mathf.Clamp(angle, min, max);
    }    
}
