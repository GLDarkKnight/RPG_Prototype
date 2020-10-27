/*CombatTarget.cs
 * 10-24-2020
 * Dependent on Core
 * RPG.Combat
 */
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    //From the Health.cs calls Health and makes sure any script that this is attached to that is also attached to.
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        //There is nothing to see here!
    }
}