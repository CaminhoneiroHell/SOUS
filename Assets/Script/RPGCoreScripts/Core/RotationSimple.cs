using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSimple : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward * 600 * Time.deltaTime, Space.Self);
    }
}
