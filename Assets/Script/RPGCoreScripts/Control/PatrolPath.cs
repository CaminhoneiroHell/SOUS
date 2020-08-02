using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float radiusGizmoz = 0.3f;

        [SerializeField] Vector3[] path;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextWayPoint(i);
                Gizmos.DrawSphere(GetWaypoint(i), radiusGizmoz);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextWayPoint(int i)
        {
            if ((i + 1) == transform.childCount)
               return 0;
            else
                return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
