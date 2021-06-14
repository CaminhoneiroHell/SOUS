using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;
using RPG.Stats;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        Mover mov;
        Animator animator;
        ActionScheduler scheduler;

        [SerializeField] Health target;

        [SerializeField] float timeAttackOffset = 2f;
        float timeSinceLastAttack = Mathf.Infinity;

        public bool targetIsInRange;

        [SerializeField] Transform rightHand = null;
        [SerializeField] Transform leftHand = null;

        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Weapon currentWeapon = null;


        private void Start()
        {
            mov = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();

            if(currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }

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
            weapon.SpawnWeapon(rightHand, leftHand, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        //Called by trigger event on the attack animation
        void Hit_AnimationEvent()
        {
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (target != null)
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        //Called by trigger event on the projectile attack animation
        void Shoot_AnimationEvent()
        {
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (target != null)
            {
                currentWeapon.LaunchProjectile(rightHand, leftHand, target, gameObject, damage);
            }
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetPercentageBonus();
            }
        }

    }

}