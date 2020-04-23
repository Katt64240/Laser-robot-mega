using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public float maxRange;
    public float rotationSpeed;

    float dist = 0;
    GameObject go;


    // Update is called once per frame
    void Update()
    {
        //manipulate object
        if(go != null)
        {
            dist *= 1+(Input.mouseScrollDelta.y * 0.25f)*Time.deltaTime;

            go.GetComponent<Rigidbody>().velocity = (Camera.main.transform.position + Camera.main.transform.forward * dist - go.transform.position) * 10f;

            if (Input.GetKey(KeyCode.E))
            {
                go.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed,Space.World);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                go.transform.Rotate(Vector3.up * Time.deltaTime * -rotationSpeed, Space.World);
            }
        }

        //PickUp up object
        if (Input.GetMouseButtonDown(0))
        {
            if (go == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxRange))
                {
                    if (hit.transform.GetComponent<Rigidbody>() != null)
                    {
                        if (hit.transform.GetComponent<Rigidbody>().isKinematic == false)
                        {
                            go = hit.transform.gameObject;
                            dist = (go.transform.position - Camera.main.transform.position).magnitude;
                        }
                    }
                }
            }
            else
            {
                //dropp object
                go = null;
            }
        }


    }
}
