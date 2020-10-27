/*PatrolPath.cs
 * 10-24-2020
 * RPG.Control
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        //Define Vars
        [SerializeField, Range(0.01f, 2)] float gizmosRadius = 0.5f;
        [SerializeField] bool ShowAlways = false;
        private int j = 1;
        //EDITOR - Will show if object is selected - Linked to ShowAlways Bool
        private void OnDrawGizmosSelected()
        {
            if (!ShowAlways)
            { 
                for (int i = 0; i < transform.childCount; i++)
                {
                    j = GetNext(i);
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(GetWaypoint(i), gizmosRadius);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
                }
            }
        }
        //EDITOR Will always show - Linked to ShowAlways Bool
        private void OnDrawGizmos()
        {
            if (ShowAlways) { 
                for (int i = 0; i < transform.childCount; i++)
                {
                    j = GetNext(i);
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(GetWaypoint(i), gizmosRadius);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
                }
            }
        }
        //GetNext will get the next transform location
        public int GetNext(int i)
        {
            if (i+1 == transform.childCount) return 0;
            return i+1;
        }
        //Get Way Point will get Current Waypoint
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
