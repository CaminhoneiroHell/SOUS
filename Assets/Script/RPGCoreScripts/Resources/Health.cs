

namespace RPG.Resources
{
    using UnityEngine;
    using RPG.Saving;
    using RPG.Stats;
    using RPG.Core;

    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;

        Animator anim;
        Collider col;
        ActionScheduler actionScheduler;

        private void Start(){

            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }

            anim = GetComponent<Animator>();
            col = GetComponent<Collider>();
            actionScheduler = GetComponent<ActionScheduler>();

            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);   //Issues
        }

        public void TakeDamage(GameObject instigator, float dmg)
        {
            healthPoints = Mathf.Max(healthPoints - dmg, 0);

            if (IsDead())
            {
                GainExperience(instigator);
                Die();
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void GainExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
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
