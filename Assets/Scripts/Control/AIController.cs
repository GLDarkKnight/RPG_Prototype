/*AIController.cs
 * 10-24-2020
 * Used for AI Control
 * Depended on Movement, Combat and Core
 * RPG.Control
 */
using UnityEngine;
using RPG.Movment;
using RPG.Combat;
using RPG.Core;
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        //Define Vars
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float wayPointDwellTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] int currentWayPoint = 0;
        [SerializeField] EnemyData data;

        private Fighter fighter;
        private GameObject player;
        private Health health;
        private Mover mover;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWayPoint = Mathf.Infinity;
        int currentWayPointIndex = 0;

        //Start
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
            chaseDistance = data.detectRange;
        }
        //Update
        private void Update()
        {
            if (health.IsDead()) return;
            GameObject player = GameObject.FindWithTag("Player");
            GameObject AIControl = gameObject;

            if (InAttackRangeofPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour(player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }
        //Updatetimer - AKA Chase and look timer
        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWayPoint += Time.deltaTime;
        }
        //Patrol Behaviour - Gets last Point and if at location move on to the next point
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArrivedAtWayPoint = 0;
                    CycleNextWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if(timeSinceArrivedAtWayPoint > wayPointDwellTime)
            {
                mover.startMoveAction(nextPosition);
            }
        }
        //Get Current Way Point - as it says
        private Vector3 GetCurrentWayPoint()
        {
            currentWayPoint = currentWayPointIndex;
            return patrolPath.GetWaypoint(currentWayPointIndex);
        }
        //Cycle Next Way Point - as it says
        private void CycleNextWayPoint()
        {
                currentWayPointIndex = patrolPath.GetNext(currentWayPointIndex);
        }
        //At Way Point - Yes again as it says
        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerance;
        }
        //Syspicion Behaviour - Stands around at last seen player spot
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        //Attack Behaviour - As it says
        private void AttackBehaviour(GameObject player)
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player.gameObject);
        }
        //Range check
        private bool InAttackRangeofPlayer()
        {
            float DistancetoPlayer = Vector3.Distance(player.transform.position, transform.position);
            return DistancetoPlayer < chaseDistance;
        }
        /*EDITOR - Draws the following
         * Guard Sight
         */
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
