using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEFX : MonoBehaviour
{
    void Update()
    {
        if (!this.GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
