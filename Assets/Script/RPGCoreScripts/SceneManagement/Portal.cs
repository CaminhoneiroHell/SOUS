using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Patterns.Creational.Singleton;
using System;
using RPG.Control;
using RPG.Core;

namespace RPG.SceneManagement
{

    public enum Destination
    {
        A, B, C, D, E
    }

    public class Portal : MonoBehaviour //Singleton<Portal>
    {
        public Transform spawnPoint;
        public Destination destination;
        [SerializeField] int sceneToLoad = 0;
        [SerializeField] float fadeOutTime = 0.3f, fadeInTime = 0.3f, fadeWaitTime = 2f;

        private SavingWrapper saveWrapper;

        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            spawnPoint = transform.GetChild(0);
            saveWrapper = FindObjectOfType<SavingWrapper>();
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

            //Save
            //SavingWrapper saveWrapper = FindObjectOfType<SavingWrapper>();
            saveWrapper.Save();

            Fader fader = FindObjectOfType<Fader>();

            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;

            Portal otherPortal = GetOtherPortal();

            yield return new WaitForSeconds(fadeWaitTime);

            fader.FadeIn(fadeInTime);
            newPlayerController.enabled = true;

            //Load
            saveWrapper.Load();

            if (fader == null)
                fader = FindObjectOfType<Fader>();
    
            UpdatePlayer(otherPortal);
            yield return fader.FadeIn(fadeInTime);

            print("FadeIn Concluded destroy this");
            Destroy(this.gameObject);

            print("Scene Loaded");

        }

        private void UpdatePlayer(Portal otherPortal)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.transform.position;
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
            print("Player updted to: " + player.transform.position);
        }

        private Portal GetOtherPortal()
        {
            //print("GetOtherPortal");
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                print("Into the: " + gameObject.name);
                return portal;
            }
            return null;
        }
    }

}