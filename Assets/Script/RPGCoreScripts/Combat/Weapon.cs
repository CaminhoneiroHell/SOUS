using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {

        [SerializeField] AnimatorOverrideController weaponOverrideAnimator = null;
        [SerializeField] GameObject weapon = null;

        [SerializeField] float damage = 5f;
        [SerializeField] float range = 2f;

        public void SpawnWeapon(Transform handTransform, Animator animator)
        {
            if(weapon != null)
            {
                Instantiate(weapon, handTransform);
            }

            if(weaponOverrideAnimator != null)
            {
                animator.runtimeAnimatorController = weaponOverrideAnimator;
            }
        }

        public float GetDamage()
        {
            return damage;
        }
        public float GetRange()
        {
            return range;
        }


    }
}
 