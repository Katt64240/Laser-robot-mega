using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BeamSegment
{
    public Vector3 p1;
    public Vector3 p2;

    public Vector3 n1;
    public Vector3 n2;


    public BeamSegment(Vector3 p1, Vector3 p2, Vector3 n1, Vector3 n2)
    {
        this.p1 = p1;
        this.p2 = p2;

        this.n1 = n1;
        this.n2 = n2;
    }
}
