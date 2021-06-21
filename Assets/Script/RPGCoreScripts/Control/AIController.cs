using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using GameDevTV.Utils;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        Fighter fight;
        Mover mover;
        Health health;
        GameObject player;
        ActionScheduler actScheduler;
        [SerializeField] PatrolPath path = null;

        LazyValue<Vector3> guardPosition = null;
        float wayPointDistanceTolerance = 1f;

        [SerializeField] float playerDistance;
        [SerializeField] float playerDistanceToChase = 2f;
        [SerializeField] float timeSinceLastSawnPlayer = Mathf.Infinity;
        [SerializeField] float suspiciousTime = 5f;
        
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.4f;

        void Awake()
        {
            health = GetComponent<Health>();
            fight = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            actScheduler = GetComponent<ActionScheduler>();

            if (player == null) 
                player = GameObject.FindGameObjectWithTag("Player");

            guardPosition = new LazyValue<Vector3>(GetGuardPosition); 
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position; 
        }

        private void Start()
        {
            guardPosition.ForceInit();
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
            spotedPlayer = false;

            Vector3 nextPosition = guardPosition.value;

            if (path == null) return;

            if (AtWayPoint())
            {
                CycleWayPoint();
                timeSinceReachedLastNode = 0;
            }
            nextPosition = GetCurrentWayPoint();

            if (timeSinceReachedLastNode > patrolTimeEachNode)
            {
                mover.MoveTo(nextPosition, patrolSpeedFraction);
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
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, playerDistanceToChase);
        }
    }

}
