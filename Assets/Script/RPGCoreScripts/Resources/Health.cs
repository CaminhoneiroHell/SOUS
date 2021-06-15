using UnityEngine;
using GameDevTV.Utils;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;


namespace RPG.Resources
{

    public class Health : MonoBehaviour, ISaveable
    {
        LazyValue<float> healthPoints;

        Animator anim;
        Collider col;
        ActionScheduler actionScheduler;
        [SerializeField] float regenerationPercentage = 70;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<Collider>();
            actionScheduler = GetComponent<ActionScheduler>();

            healthPoints.ForceInit();

            //GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            //healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            //if (healthPoints < 0)
            //{
            //    healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            //}

            //healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);   //Issues
        }

        private float GetInitialHealth()
        {
             return healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }


        public void TakeDamage(GameObject instigator, float dmg)
        {
            print(gameObject.name + " took damage: " + dmg);
            healthPoints.value = Mathf.Max(healthPoints.value - dmg, 0);

            if (IsDead())
            {
                GainExperience(instigator);
                Die();
            }
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
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
            return healthPoints.value <= 0; //&& anim.GetBool("Dead");
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

            healthPoints.value = (float)state;

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

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

    }
}
