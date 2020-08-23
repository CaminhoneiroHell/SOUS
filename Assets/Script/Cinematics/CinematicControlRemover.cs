
namespace RPG.Cinematics
{
    using RPG.Control;
    using RPG.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
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
            if (director == aDirector)
                Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnDisable()
        {
            director.played -= DisableControl;
            director.stopped -= EnableControl;
        }
    }

}
