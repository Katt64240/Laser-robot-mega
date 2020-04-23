using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if object is out of bounds
        if(transform.position.magnitude > 100f)
        {
            Application.LoadLevel(Application.loadedLevel);
            Debug.Log("Object out of bounds, restarting level");
        }
    }
}
