/*Fighter.cs
 * 10-24-2020
 * Dependent on Movement, and Core
 * RPG.Combat
 */
using UnityEngine;
using RPG.Movment;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] EnemyData data;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool ishostile = false;
        private Mover mover;
        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;
        //Start setup
        private void Start()
        {
            mover = GetComponent<Mover>();
            weaponDamage = data.weaponDamage;
        }
        //standard update setting
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }
        //The Attack Behaviour
        private void AttackBehaviour()
        {
            this.transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                AttackTrigger();
                timeSinceLastAttack = 0;
            }
        }
        //Triggers the Attack in the animaion loop.
        private void AttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        //Check if the target we have we can attack it if not ignore it
        public bool isHostile()
        {
            return ishostile;
        }
        //Can Attack - Well can I?
        public bool CanAttack(GameObject combat)
        {
            if (combat == null) return false;
            Health testTarget = combat.GetComponent<Health>();
            return testTarget != null && !testTarget.IsDead();
        }
        //Set the target
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }
        //Cancel the target
        public void Cancel()
        {
            StopAttackTrigger();
            target = null;
        }
        //Stop the Attack
        private void StopAttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}