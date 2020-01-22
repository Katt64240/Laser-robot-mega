using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int frequency = 0;

    public Transform[] GetExitPortals()
    {
        GameObject[] allPortals = GameObject.FindGameObjectsWithTag("Portal");

        List<Transform> resonantPortals = new List<Transform>();

        for(int i = 0; i < allPortals.Length; i++)
        {
            if(allPortals[i].GetComponent<Portal>().frequency == frequency && !allPortals[i].Equals(gameObject))
            {
                resonantPortals.Add(allPortals[i].transform);
            }
        }
        return resonantPortals.ToArray();
    }
}
