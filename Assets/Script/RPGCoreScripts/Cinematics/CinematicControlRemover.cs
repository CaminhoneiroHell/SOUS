
namespace RPG.Cinematics
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using RPG.Core;
    using UnityEngine;
    using RPG.Control;
    using UnityEngine.Playables;

    public class CinematicControlRemover : MonoBehaviour
    {
        public PlayableDirector director;
        void OnEnable()
        {
            director.played += DisableControl;
            director.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector aDirector)
        {

            GameObject go = GameObject.FindGameObjectWithTag("Player");
            go.GetComponent<ActionScheduler>().CancelCurrentAction();
            go.GetComponent<PlayerController>().enabled = false;


#if DEBUG
            if (director == aDirector)
                Debug.Log("PlayableDirector named " + aDirector.name + " is now playing.");
#endif
        }

        void EnableControl(PlayableDirector aDirector)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            go.GetComponent<PlayerController>().enabled = true;

            if (director == aDirector)
                Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
        }

        private void OnDisable()
        {
            director.played -= DisableControl;
            director.stopped -= EnableControl;
        }

    }

}
