

namespace RPG.Core
{
    using RPG.Combat;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using RPG.Saving;

    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        Animator anim;
        Collider col;
        ActionScheduler actionScheduler;

        private void Start(){
            anim = GetComponent<Animator>();
            col = GetComponent<Collider>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float dmg)
        {
            healthPoints = Mathf.Max(healthPoints - dmg, 0);

            if (IsDead())
            {
                Die();
            }
        }

        private void Die()
        {
            anim.SetTrigger("Die");
            actionScheduler.CancelCurrentAction();
        }

        public bool IsDead()
        {
            return healthPoints <= 0; //&& anim.GetBool("Dead");
        }

        //Animation event caller
        public void SetAnimToDead()
        {
            col.enabled = false;    
            anim.SetBool("Dead", true);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {

            healthPoints = (float)state;

            if (IsDead())
            {
                //Dies on load

                Die(); 
                SetAnimToDead();
                anim.SetBool("Dead", false);
            }
            else
            {
                //Ressurect on load
                col.enabled = true;
                anim.SetBool("Dead", false);
            }
        }
    }
}
