using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;


        NavMeshAgent navMesh;
        ActionScheduler scheduler;
        Health health;

        void Update()
        {
            navMesh.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination/*, float speedFraction*/)
        {
            //target.transform.position = destination;

            scheduler.StartAction(this);
            navMesh.destination = destination;
            //navMesh.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMesh.isStopped = false;
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            //target.transform.position = destination;

            scheduler.StartAction(this);
            navMesh.destination = destination;
            navMesh.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMesh.isStopped = false;
        }

        public void Stop()
        {
            navMesh.isStopped = true;
            //print(" Nothing shall remain thy name.");
        }


        Animator animator;
        private void UpdateAnimator()
        {
            Vector3 velocity = navMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("fowardSpeed", speed);    
        }

        void Start()
        {
            navMesh = gameObject.GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        public void Cancel()
        {

        }
    }
}
 