using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{

    public bool IsActivated = true;

    LineRenderer lr;

    float maxRayDistance = 1000f;
    float rayWidth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>(); // använd egen cylinder mesh class
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated)
        {
            UpdateLine();
        }
    }

    void UpdateLine()//uppdatera med mer funktionalitet
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(transform.position);


        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit, maxRayDistance)) // använd spherecast istället
        {
            points.Add(hit.point);
        }
        else
        {
            points.Add(transform.position+transform.forward* maxRayDistance);
        }

        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());

        lr.SetWidth(rayWidth, rayWidth);
    }
}
