using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{

    public bool IsActivated = true;

    MeshFilter mf;

    float maxRayDistance = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated)
        {
            UpdateLine();
        }
    }

    void UpdateLine()
    {

        mf.mesh = BeamMesh.GenerateMesh(RecursiveCal(transform.position, transform.forward, transform.forward, 10, 5).ToArray(), transform);

    }

    List<BeamSegment> RecursiveCal(Vector3 point, Vector3 dir, Vector3 normal, int maxBounce, int maxPortal)
    {
        BeamSegment segment = new BeamSegment(point, point + dir * maxRayDistance, normal, -dir);
        List<BeamSegment> segments = new List<BeamSegment>();

        RaycastHit hit;
        if (Physics.Raycast(point, dir, out hit, maxRayDistance))
        {
            segment.p2 = hit.point;
            segment.n2 = hit.normal;

            if(hit.transform.tag == "Reflector" && maxBounce >= 1)
            {
                segments.AddRange(RecursiveCal(hit.point, Vector3.Reflect(dir, hit.normal), hit.normal, maxBounce - 1, maxPortal));
                
            }

            if (hit.transform.tag == "Portal" && maxPortal >= 1)
            {
                Transform[] portals = hit.transform.GetComponent<Portal>().GetExitPortals();

                for(int i = 0; i < portals.Length; i++)
                {
                    Vector3 exitDir = Vector3.Reflect(dir, hit.normal);
                    Vector3 exitPoint = hit.point;

                    exitDir = hit.transform.InverseTransformDirection(exitDir);
                    exitPoint = hit.transform.InverseTransformPoint(exitPoint);
                    //exitPoint.x *= -1;

                    exitDir = portals[i].TransformDirection(exitDir);
                    exitPoint = portals[i].TransformPoint(exitPoint);

                    //                                           ------Portal normal------
                    segments.AddRange(RecursiveCal(exitPoint, exitDir, portals[i].up, maxBounce , maxPortal - 1));
                }
            }

            if(hit.transform.tag == "Sensor")
            {
                hit.transform.GetComponent<Sensor>().isHitByLaser = true;
            }
        }

        segments.Add(segment);

        return segments;
    }


}
