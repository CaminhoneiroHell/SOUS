using RPG.Core;
using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {

        [SerializeField] AnimatorOverrideController weaponOverrideAnimator = null;
        [SerializeField] GameObject equipedWeapon = null;

        [SerializeField] float damage = 5f;
        [SerializeField] float range = 2f;

        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void SpawnWeapon(Transform rHand, Transform lHand, Animator animator)
        {
            DestroyOldWeapon(rHand, lHand);

            if(equipedWeapon != null)
            {
                Transform hTransform = GetTransform(rHand, lHand);
                GameObject weapon = Instantiate(equipedWeapon, hTransform);
                weapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (weaponOverrideAnimator != null)
            {
                animator.runtimeAnimatorController = weaponOverrideAnimator;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rHand,Transform lHand)
        {
            Transform oldWeapon = rHand.Find(weaponName);
            
            if (oldWeapon == null)
                oldWeapon = lHand.Find(weaponName);

            if (oldWeapon == null)
                return;

            oldWeapon.name = "DESTROYING"; //This solve a solve a bug that destroy the current weapon we are picking
            Destroy(oldWeapon.gameObject);
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

        public void LaunchProjectile(Transform rHand, Transform lHand, Health target, GameObject instigator, float calculateDamage)
        {
            Projectile projectileInstance = Instantiate(projectile,
                                                        GetTransform(rHand, lHand).position,
                                                                        Quaternion.identity);

            projectileInstance.SetTarget(target, calculateDamage, instigator);
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
 