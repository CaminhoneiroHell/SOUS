namespace RPG.Cinematics
{
    using System.Collections;
    using System.Collections.Generic;
    using RPG.Saving;
    using UnityEngine;
    using UnityEngine.Playables;

    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool action;

        public object CaptureState()
        {
            return action;
        }

        public void RestoreState(object state)
        {
            bool b = action;
            action = (bool)state;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && !action)
            {
                GetComponent<PlayableDirector>().Play();
                action = true;
            }
        }
    }

}
