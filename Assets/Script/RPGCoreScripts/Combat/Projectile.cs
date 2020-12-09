using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using RPG.Resources;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] Health target = null;
    [SerializeField] bool isHoming = true;
    private float damage;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        if (isHoming && !target.IsDead()) { transform.LookAt(GetAimLocation()); }
    
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }

        return target.transform.position + Vector3.up * targetCapsule.height/1.7f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        if (target.IsDead()) return;

        target.TakeDamage(damage);
        Destroy(gameObject);
        
    }
}   
