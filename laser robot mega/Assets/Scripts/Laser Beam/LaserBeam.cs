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
        mf.mesh = BeamMesh.GenerateMesh(RecursiveCal(transform.position, transform.forward, 10, 5).ToArray(), transform);
    }

    //Gets list of beam segments given a source and laser direction
    List<BeamSegment> RecursiveCal(Vector3 point, Vector3 dir, int maxBounce, int maxPortal)
    {
        BeamSegment segment = new BeamSegment(point, point + dir * maxRayDistance);
        List<BeamSegment> segments = new List<BeamSegment>();

        RaycastHit hit;
        if (Physics.Raycast(point, dir, out hit, maxRayDistance))
        {
            segment.p2 = hit.point;

            if(hit.collider.transform.tag == "Reflector" && maxBounce >= 1)
            {
                segments.AddRange(RecursiveCal(hit.point, Vector3.Reflect(dir, hit.normal), maxBounce - 1, maxPortal));
            }

            if (hit.collider.transform.tag == "Portal" && maxPortal >= 1)
            {
                Transform[] portals = hit.collider.transform.GetComponent<Portal>().GetExitPortals();

                for(int i = 0; i < portals.Length; i++)
                {
                    Vector3 exitDir = -portals[i].transform.forward;
                    Vector3 exitPoint = portals[i].transform.position;

                    segments.AddRange(RecursiveCal(exitPoint, exitDir, maxBounce , maxPortal - 1));
                }
            }

            if(hit.transform.tag == "Sensor")
            {
                hit.transform.GetComponent<Sensor>().isHitByLaser = true;
            }

            if(hit.transform.tag == "Colectable")
            {
                GameManager.colectables++;
                Destroy(hit.transform.gameObject);
            }
        }

        segments.Add(segment);

        return segments;
    }


}
