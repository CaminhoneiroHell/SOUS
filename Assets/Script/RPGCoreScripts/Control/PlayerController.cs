
namespace RPG.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    using System;
    using RPG.Movement;
    using RPG.Combat;
    using RPG.Core;
    using RPG.Resources;

    public class PlayerController : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        Mover mover;
        Fighter warrior;
        CombatTarget target;
        Health health;

        void Start()
        {
            mover = GetComponent<Mover>();
            warrior = GetComponent<Fighter>();
            health = GetComponent<Health>();

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }


        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (!mover.isActiveAndEnabled) return;
            if (InteractiveWithCombat()) return;
            if (InteractiveWithMovement()) return;
        }

        private bool InteractiveWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                GameObject targetGO = target.gameObject;

                if (!warrior.CanAttack(targetGO))
                {
                    continue;
                }
                
                if(hit.collider.tag == "Thug")
                {
                    if (Input.GetMouseButton(0))
                    {
                        warrior.Attack(targetGO);
                    }
                    return true;
                }
            }
            return false;
        }

        bool InteractiveWithMovement()
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButton(0))
                {
                    ray = GetMouseRay();
                    mover.MoveTo(hit.point/*, 1f*/);
                }
                return true;
            }
            print("Nothing to do...");
            return false;
        }
    }
}
