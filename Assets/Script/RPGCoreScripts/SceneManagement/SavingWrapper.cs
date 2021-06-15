using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        [SerializeField] float fadeInTime = 0.2f;

        //private void Awake()
        //{
        //    StartCoroutine(LoadLastScene());
        //}

        //private IEnumerator LoadLastScene()
        //{
        //    yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        //    Fader fader = FindObjectOfType<Fader>();
        //    StartCoroutine(fader.FadeInOut());
        //    yield return fader.FadeIn(fadeInTime);
        //}

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            //Calol to saving system load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }

    }

}

