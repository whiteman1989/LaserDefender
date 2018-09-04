using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour, IPylon {
    [SerializeField] float sizeGizmo = 0.05f;
    [SerializeField] int pylonId;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sizeGizmo);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(Vector3.up*sizeGizmo*2));
    }

    public Transform GetPylonPosition()
    {
        return transform;
    }

    public int GetPylonId()
    {
        return pylonId;
    }

}
