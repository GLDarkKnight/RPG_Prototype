/*FollowCamera.cs
 * 10-24-2020
 * RPG.Core
 * NOTE this follows the player and is simple system.
 */
using UnityEngine;

namespace RPG.Core
{
    [ExecuteInEditMode]
    public class FollowCamera : MonoBehaviour
    {
        //Define Vars-
        //Note can also get transform from find tag player!
        [SerializeField] Transform target;
        //Follow the Target
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}