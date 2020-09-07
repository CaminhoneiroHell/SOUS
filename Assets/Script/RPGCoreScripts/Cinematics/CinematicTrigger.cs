using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        bool action;

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
