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
    [SerializeField] GameObject hitEffect = null;
    private float damage;


    [SerializeField] float maxLifeTime = 10;
    [SerializeField] GameObject[] destroyOnHit = null;
    [SerializeField] float lifeAfterImpact = 2;

    GameObject instigator = null;

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

    public void SetTarget(Health target, float damage, GameObject instigator)
    {
        this.target = target;
        this.damage = damage;
        this.instigator = instigator;


        Destroy(gameObject, maxLifeTime);
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


        if (hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }


        target.TakeDamage(instigator, damage);

        foreach (GameObject toDestroy in destroyOnHit)
        {
            Destroy(toDestroy);
        }

        Destroy(gameObject/*, lifeAfterImpact*/);

    }
}   
