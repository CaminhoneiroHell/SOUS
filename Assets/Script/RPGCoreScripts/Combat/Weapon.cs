using RPG.Core;
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
        [SerializeField] Projectile projectile = null;

        public void SpawnWeapon(Transform rHand, Transform lHand, Animator animator)
        {
            if(weapon != null)
            {
                Transform hTransform = GetTransform(rHand, lHand);
                Instantiate(weapon, hTransform);
            }

            if (weaponOverrideAnimator != null)
            {
                animator.runtimeAnimatorController = weaponOverrideAnimator;
            }
        }

        private Transform GetTransform(Transform rHand, Transform lHand)
        {
            Transform hTransform;
            if (isRightHand) hTransform = rHand;
            else hTransform = lHand;
            return hTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
        public void LaunchProjectile(Transform rHand, Transform lHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile,
                                                        GetTransform(rHand, lHand).position,
                                                                        Quaternion.identity);

            projectileInstance.SetTarget(target, GetDamage());
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
 