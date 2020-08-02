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
        NavMeshAgent navMesh;
        ActionScheduler scheduler;
        Health health;

        void Update()
        {
            navMesh.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            scheduler.StartAction(this);
            //target.transform.position = destination;
            navMesh.destination = destination;
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
 