using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSimple : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.left * 600 * Time.deltaTime, Space.Self);
    }
}
