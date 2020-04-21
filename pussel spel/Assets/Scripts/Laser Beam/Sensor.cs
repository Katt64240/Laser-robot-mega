using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{

    public Material disabledMterial;
    public Material enabledMaterial;

    public GameObject targetDisableObject;

    public bool isHitByLaser = false;


    // Update is called once per frame
    void Update()
    {
        if (isHitByLaser)
        {
            GetComponent<MeshRenderer>().material = enabledMaterial;
            targetDisableObject.active = false;
        }
        else
        {
            GetComponent<MeshRenderer>().material = disabledMterial;
            targetDisableObject.active = true;
        }
        isHitByLaser = false;
    }

}
