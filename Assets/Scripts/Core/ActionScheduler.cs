using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction = null;

        public void StartAction(IAction newAction)
        {
            if (currentAction == newAction)
                return;

            if(currentAction != null)
            {
                print("ActionScheduler: Cancelling Action (" + currentAction + ")");
                currentAction.Cancel();

            }

            currentAction = newAction;
            print("ActionScheduler: Starting Action (" + currentAction + ")");
        }

        public void CancelAction()
        {
            StartAction(null);
        }
    }
}
