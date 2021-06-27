
namespace RPG.Core
{
    using UnityEngine;

    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction action) //When cancelling action comes null
        {
            //If is the same action that has been called, ignore
            if (currentAction == action) return;

            if(currentAction != null)
            {
                print("Canceling current action"
                    + currentAction);

                currentAction.Cancel();
            }
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
            print("Forcing Cancel current action"
                + currentAction);
        }
    }
}
