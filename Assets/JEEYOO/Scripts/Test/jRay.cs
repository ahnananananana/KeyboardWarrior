using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jRay : MonoBehaviour
{
    private float smoothSpeed = 10f;
    private float moveSpeed = 20f;
    private float dashSpeed = 8f;
    private bool isCrash = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        PointTo();
        MoveTo();
        MoveWithKeyboard();
    }

    private void PointTo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(hit.point);
        }
    }

    private void MoveTo()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 position = transform.position;
                position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.position = Vector3.Lerp(transform.position, position, 0.2f * smoothSpeed * Time.smoothDeltaTime);
                
            }
        }
    }

    private void MoveWithKeyboard()
    {
        Vector3 position = transform.position;
        position.x += Input.GetAxisRaw("Horizontal") * Time.smoothDeltaTime * moveSpeed;
        position.z += Input.GetAxisRaw("Vertical") * Time.smoothDeltaTime * moveSpeed;
        transform.position = position;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(CrashBox());
            //Vector3 position = transform.position;
            //position.x += Input.GetAxisRaw("Horizontal") * dashSpeed;
            //position.z += Input.GetAxisRaw("Vertical") * dashSpeed;
            //transform.position = Vector3.Lerp(transform.position, position, 0.5f * Time.smoothDeltaTime * dashSpeed);
        }
    }

    private void crash()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 targetVector = hit.point - transform.position;
            targetVector.Normalize();
            transform.position += targetVector * 10f * Time.deltaTime;
            //transform.position = Vector3.Lerp(transform.position, hit.point, 0.5f * Time.smoothDeltaTime * dashSpeed);
        }
    }

    IEnumerator CrashBox()
    {
        while (isCrash)
        {
            float dist = 0;
            float targetDist = 0;
            Vector3 startPosition;
            bool isInit = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 targetVector = hit.point - transform.position;
                targetVector.Normalize();
                if (!isInit)
                {
                    targetDist = Vector3.Distance(hit.point, transform.position);
                    startPosition = transform.position;
                    isInit = true;
                }

                transform.position += targetVector * 50f * Time.deltaTime;
                //transform.position = Vector3.Lerp(transform.position, hit.point, 0.5f * Time.smoothDeltaTime * dashSpeed);
            }
            yield return null;
        }
    }
}
