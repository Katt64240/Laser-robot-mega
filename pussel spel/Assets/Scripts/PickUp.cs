using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public float maxRange;
    public Vector2 rotationSpeed;

    float dist = 0;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(go != null)
        {
            go.transform.Rotate(Camera.main.transform.right * Input.mouseScrollDelta.y * rotationSpeed.x, Space.World);

            if (Input.GetKey(KeyCode.E))
            {
                go.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed.y,Space.World);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                go.transform.Rotate(Vector3.up * Time.deltaTime * -rotationSpeed.y, Space.World);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (go == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxRange))
                {
                    if (hit.transform.GetComponent<Rigidbody>() != null)
                    {
                        go = hit.transform.gameObject;
                        dist = (go.transform.position - Camera.main.transform.position).magnitude;
                        go.transform.parent = Camera.main.transform;
                        go.transform.localPosition = Vector3.forward * dist;
                        go.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
            else
            {
                go.GetComponent<Rigidbody>().isKinematic = false;
                go.transform.parent = null;
                go = null;
            }
        }


    }
}
