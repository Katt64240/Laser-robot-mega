using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BeamSegment
{
    public Vector3 p1;
    public Vector3 p2;

    public BeamSegment(Vector3 p1, Vactor3 p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }
}
