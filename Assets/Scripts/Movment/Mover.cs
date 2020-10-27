/*Mover.cs
 * 10-24-2020
 * Depended on Core
 * RPG.Movment
 * Core Movement system
 */
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movment
{
    public class Mover : MonoBehaviour, IAction
    {
        //Define the Vars
        Health health;
        private NavMeshAgent navMeshAgent;
        //Start
        private void Start()
        {
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        //Update
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }
        //Updates the movement
        void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localvelocity = transform.InverseTransformDirection(velocity);
            float lspeed = localvelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", lspeed);
        }
        //Stops the Mesh angent
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        //Sets the location for the agent to move to.
        public void MoveTo (Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }
        //Starts the Move action - Call this function to move the agent.
        public void startMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
    }
}