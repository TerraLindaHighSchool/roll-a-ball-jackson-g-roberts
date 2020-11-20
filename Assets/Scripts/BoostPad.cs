using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    public float strength;
    
    public Vector3 getForwardVector()
    {
        return transform.forward * strength;
    }
}
