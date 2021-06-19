using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Resources;
using UnityEngine.EventSystems;
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        Mover mover;
        Fighter warrior;
        CombatTarget target;
        Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        void Awake()
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

            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            if (!mover.isActiveAndEnabled) return;
            if (InteractWithUI()) return;
            //if (InteractiveWithCombat()) return;
            if (InteractWithComponent()) return;
            if (InteractiveWithMovement()) return;


            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            //RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(CursorType.Combat);
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        //private bool InterfactWithCombat()
        //{
        //    RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        //    foreach (RaycastHit hit in hits)
        //    {
        //        target = hit.transform.GetComponent<CombatTarget>();
        //        if (target == null) continue;

        //        GameObject targetGO = target.gameObject;

        //        if (!warrior.CanAttack(targetGO))
        //        {
        //            continue;
        //        }

        //        if(hit.collider.tag == "Thug")
        //        {
        //            SetCursor(CursorType.Combat);
        //            if (Input.GetMouseButton(0))
        //            {
        //                warrior.Attack(targetGO);
        //            }
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        bool InteractiveWithMovement()
        {
            if (Physics.Raycast(ray, out hit))
            {
                SetCursor(CursorType.Movement);
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

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
    }
}
