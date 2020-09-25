using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Mover mov;
        Animator animator;
        Health target;
        ActionScheduler scheduler;

        //float weaponDamage = 5f;
        //[SerializeField] float attackDistanceOffset =4f;

        [SerializeField] float timeAttackOffset = 2f;
        float timeSinceLastAttack = Mathf.Infinity;

        public bool targetIsInRange;

        [SerializeField] Transform handPosition = null;
        [SerializeField] Weapon defaultWeapon = null;
        
        Weapon currentWeapon = null;

        private void Start()
        {
            mov = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();

            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            if (GetComponent<Health>().IsDead()) return;

            timeSinceLastAttack += Time.deltaTime;
            if (target != null)
            {
                if (!TargetIsInRange())
                {
                    mov.MoveTo(target.transform.position, 1f);
                }
                else
                {
                    //Being called around 27 frames p/ second
                    scheduler.StartAction(this);
                    mov.Stop();
                    if (timeAttackOffset < timeSinceLastAttack)
                    {
                        transform.LookAt(target.transform);
                        animator.SetTrigger("Attack");
                        timeSinceLastAttack = 0;
                    }
                }
            }
        }

        private bool TargetIsInRange()
        {
            targetIsInRange = TargetDistance() <= currentWeapon.GetRange();
            return targetIsInRange;
        }

        private float TargetDistance()
        {
            return Vector3.Distance(target.transform.position, transform.position);
        }

        public void Attack(GameObject combatTarget)
        {
            //print(" Come take a taste of my cold steel, jackal!"); 
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Cancel()
        {
            target = null;
            animator.SetTrigger("OutOfAttack");
            GetComponent<Mover>().Cancel();
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            weapon.SpawnWeapon(handPosition, animator);
        }

        //Called by trigger event on the attack animation
        void Hit_AnimationEvent()
        {
            if (target != null)
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

    }

}