using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BeamMesh
{
    private static readonly int lod = 8; 
    private static readonly float beamRadious = 0.05f;

    //creates the laser mesh from array of beam segments
    public static Mesh GenerateMesh(BeamSegment[] segments, Transform origo)
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();


        for(int i = 0; i < segments.Length; i++)
        {

            Vector3[] cylindePoints = GenerateCylinderPoints(segments[i], origo);

            int l = vertices.Count;

            for (int p = 0; p < lod; p++)
            {

                triangles.Add(l + p);
                triangles.Add(l + p + lod);
                triangles.Add(l + (p+1)%lod + lod);

                triangles.Add(l + p);
                triangles.Add(l + (p + 1) % lod + lod);
                triangles.Add(l + (p + 1) % lod);
            }

            vertices.AddRange(cylindePoints);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        return mesh;
    }

    //Creates a cylinder points from a beam segment transformed to local space
    static Vector3[] GenerateCylinderPoints(BeamSegment segment, Transform origo)
    {
        Vector3[] cylindePoints = new Vector3[lod * 2];

        Vector3 p1 = segment.p1;
        Vector3 p2 = segment.p2;

        Vector3 dir = (p2 - p1).normalized;

        float angleSegment = (2 * Mathf.PI) / lod;

        Vector3 axis1 = FindPerpendicularVector(p2-p1, Vector3.up).normalized;
        Vector3 axis2 = Vector3.Cross(axis1, p2-p1).normalized;


        for (int p = 0; p < lod; p++)
        {
            //create rings
            cylindePoints[p] = axis1 * Mathf.Cos(angleSegment * p) * beamRadious
                + axis2 * Mathf.Sin(angleSegment * p) * beamRadious;

            cylindePoints[p + lod] = axis1 * Mathf.Cos(angleSegment * p) * beamRadious
                + axis2 * Mathf.Sin(angleSegment * p) * beamRadious;

            //place points
            cylindePoints[p] += p1;
            cylindePoints[p + lod] += p2;

            //transform points to local space
            cylindePoints[p] = origo.InverseTransformPoint(cylindePoints[p]);
            cylindePoints[p + lod] = origo.InverseTransformPoint(cylindePoints[p + lod]);
        }

        return cylindePoints;
    }

    //finds a perpendicular vector even if v1, v2 are paralell
    static Vector3 FindPerpendicularVector(Vector3 v1, Vector3 v2)
    {
        if (Vector3.Cross(v1, v2) != Vector3.zero)
        {
            return Vector3.Cross(v1, v2).normalized;
        }
        else
        {
            if (Vector3.Cross(v1, Vector3.up) != Vector3.zero)
            {
                return Vector3.Cross(v1, Vector3.up).normalized;
            }
            else
            {
                return Vector3.Cross(v1, Vector3.forward).normalized;
            }
        }
    }

}
