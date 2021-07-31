using System;
using RPG.Core;
using RPG.Stats;
using RPG.Saving;
using UnityEngine;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine.AI;
using GameDevTV.Utils;
using System.Collections;
using System.Collections.Generic;
using UniversalZero.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        Mover mov;
        Animator animator;
        ActionScheduler scheduler;

        [SerializeField] Health target;

        [SerializeField] float timeAttackOffset = 2f; // Can cause double attack triggers when the timeAttackOffset is slower than total attack animation time

        [SerializeField] float timeSinceLastAttack = Mathf.Infinity;

        public bool targetIsInRange;

        [SerializeField] Transform rightHand = null;
        [SerializeField] Transform leftHand = null;

        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] LazyValue<Weapon> currentWeapon;

        HostileBehaviour hostileBehaviour;



        //private void OnEnable()
        //{
        //    BattleMediator.StartBattleEvent battleEvent;
        //}

        //private void SetDe

        private void Awake()
        {
            mov = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();
            hostileBehaviour = GetComponent<HostileBehaviour>();

            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);

        }

        private Weapon SetupDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }

        bool duellingFlag = false;
        private void Update()
        {
            if (GetComponent<Health>().IsDead()) return;

            timeSinceLastAttack += Time.deltaTime;
            if (target != null && !target.IsDead())
            {
                if (!TargetIsInRange())
                {
                    mov.MoveTo(target.transform.position, 1f);
                }
                else
                {
                    //Being called around 27 frames p/ second
                    mov.Stop();
                    if (timeAttackOffset < timeSinceLastAttack && !duellingFlag)
                    {
                        scheduler.StartAction(this);
                        //print("Moveto called on update!");
                        transform.LookAt(target.transform);
                        //animator.SetTrigger("Attack");
                        //timeSinceLastAttack = 0;

                        switch (currentWeapon.value.name)
                        {
                            case "Sword":
                                if (hostileBehaviour.enabled)
                                {
                                    duellingFlag = true;
                                    hostileBehaviour.StartHostileBehaviour(target.gameObject);
                                    timeSinceLastAttack = 0;
                                }
                                else
                                {
                                    BaseAttack();
                                }
                                break;
                            default:
                                BaseAttack();
                                break;
                        }
                    }
                }
            }
        }

        private void BaseAttack()
        {
            animator.SetTrigger("Attack");
            timeSinceLastAttack = 0;
        }

        //private void Update()
        //{
        //    if (GetComponent<Health>().IsDead()) return;

        //    timeSinceLastAttack += Time.deltaTime;
        //    if (target != null && !target.IsDead())
        //    {
        //        if (!TargetIsInRange())
        //        {
        //            mov.MoveTo(target.transform.position, 1f);
        //        }
        //        else
        //        {
        //            //Being called around 27 frames p/ second
        //            mov.Stop();
        //            if (timeAttackOffset < timeSinceLastAttack)
        //            {
        //                scheduler.StartAction(this);
        //                print("Moveto called on update!");
        //                transform.LookAt(target.transform);
        //                animator.SetTrigger("Attack");
        //                timeSinceLastAttack = 0;
        //            }
        //        }
        //    }
        //}

        private bool TargetIsInRange()
        {
            targetIsInRange = TargetDistance() <= currentWeapon.value.GetRange();
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
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
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
            if (target != null && !duellingFlag)
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        //Called by trigger event on the projectile attack animation
        void Shoot_AnimationEvent()
        {
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (target != null /*&& !duellingFlag*/)
            {
                currentWeapon.value.LaunchProjectile(rightHand, leftHand, target, gameObject, damage);
            }
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
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
                yield return currentWeapon.value.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }

    }

}