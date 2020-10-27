/*ActionScheduler.cs
 * 10-24-2020
 * Core System
 * RPG.Core
 */
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        //Define Vars
        IAction currentAction;
        //Tells the game what action it is doing
        public void StartAction (IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
        //Stops current action what ever it is
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
