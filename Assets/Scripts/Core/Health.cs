/*Health.cs
 * Used for getting health releated information
 * RPG.Core system
 */
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        //Define Vars
        [SerializeField] EnemyData data;
        [SerializeField] float health = 100f;
        private bool isDead = false;
        //Return if target is dead
        private void Start()
        {
            health = data.health;
        }
        public bool IsDead()
        {
            return isDead;
        }
        //Take Damage function will get both damage and max health then return number min is 0
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                Die();
            }
        }
        //Set the trigger to die when target dies
        private void Die()
        {
            if (isDead == true)
                return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
