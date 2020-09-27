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

        [SerializeField] bool isRightHand = true;

        public void SpawnWeapon(Transform rHand, Transform lHand, Animator animator)
        {
            if(weapon != null)
            {
                Transform hTransform;

                if (isRightHand) hTransform = rHand;
                else hTransform = lHand;

                Instantiate(weapon, hTransform);
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
 