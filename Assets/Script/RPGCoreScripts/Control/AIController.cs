

namespace RPG.Control
{
    using RPG.Combat;
    using RPG.Core;
    using RPG.Movement;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AIController : MonoBehaviour
    {
        Fight fight;
        Mover mover;
        Health health;
        ActionScheduler actScheduler;
        GameObject player;
        [SerializeField] PatrolPath path = null;

        Vector3 guardPosition;
        float wayPointDistanceTolerance = 1f;

        float playerDistance;
        [SerializeField] float playerDistanceToChase = 2f;
        [SerializeField] float timeSinceLastSawnPlayer = Mathf.Infinity;
        [SerializeField] float suspiciousTime = 5f;

        void Start()
        {
            health = GetComponent<Health>();
            fight = GetComponent<Fight>();
            mover = GetComponent<Mover>();
            actScheduler = GetComponent<ActionScheduler>();

            if (player == null) 
                player = GameObject.FindGameObjectWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead())
                return;

            AI_InteractiveWithCombat();

            UpdateTimers();

        }

        private void UpdateTimers()
        {
            timeSinceLastSawnPlayer += Time.deltaTime;

            if (path != null)
                timeSinceReachedLastNode += Time.deltaTime;
        }

        bool spotedPlayer = false;
        private void AI_InteractiveWithCombat()
        {
            //If target is in range chase it
            if (InAttackRange()  && fight.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (spotedPlayer)
            {
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            print("Calling AttackBehaviour");

            spotedPlayer = true;
            timeSinceLastSawnPlayer = 0;
            fight.Attack(player);
        }

        private void SuspiciousBehaviour()
        {
            print("Calling SuspiciousBehaviour");
            actScheduler.CancelCurrentAction();
    
            if(suspiciousTime <= timeSinceLastSawnPlayer)
                PatrolBehaviour();
        }

        private void PatrolBehaviour()
        {
            print("Calling PatrolBehaviour");
            spotedPlayer = false;

            Vector3 nextPosition = guardPosition;

            if (path == null) return;

            if (AtWayPoint())
            {
                CycleWayPoint();
                timeSinceReachedLastNode = 0;
            }
            nextPosition = GetCurrentWayPoint();

            if(timeSinceReachedLastNode > patrolTimeEachNode)
            {
                mover.MoveTo(nextPosition); 
            }
        }

        int currentWayPointIntex = 0;
        [SerializeField] float timeSinceReachedLastNode = Mathf.Infinity;
        [SerializeField] float patrolTimeEachNode = 4f;
        private Vector3 GetCurrentWayPoint()
        {
            return path.GetWaypoint(currentWayPointIntex);
        }

        private void CycleWayPoint()
        {
            currentWayPointIntex = path.GetNextWayPoint(currentWayPointIntex);
        }

        private bool AtWayPoint()
        {
            float distance = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distance <= wayPointDistanceTolerance;
        }

        private bool InAttackRange()
        {
            playerDistance = Vector3.Distance(transform.position,
                player.transform.position);

            return playerDistance <= playerDistanceToChase;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, playerDistanceToChase);
        }
    }

}
