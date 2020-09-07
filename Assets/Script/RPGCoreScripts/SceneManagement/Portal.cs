

namespace RPG.SceneManagement
{
    using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;
    using System;
    using RPG.Core;

    public enum Destination
    {
        A, B, C, D, E
    }

    public class Portal : MonoBehaviour
    {
        public Transform spawnPoint;
        public Destination destination;
        
        [SerializeField] int sceneToLoad = 0;
        [SerializeField] float fadeOutTime = 0.3f, fadeInTime = 0.3f;

        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            spawnPoint = transform.GetChild(0);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(SceneTransition());
            }
        }

        public IEnumerator SceneTransition()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            yield return fader.FadeIn(fadeInTime);
            
            print("Scene Loaded");
            Destroy(gameObject);

        }

        private void UpdatePlayer(Portal otherPortal)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.transform.position;
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;

            }

            return null;
        }
    }

}