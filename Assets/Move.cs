using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Collision col;
    Rigidbody rigM;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collision>();
        rigM = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rigM.AddForce(0,11,0);
        }
    }
}
