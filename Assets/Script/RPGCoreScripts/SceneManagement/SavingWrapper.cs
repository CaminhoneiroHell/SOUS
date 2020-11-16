using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;


namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        //IEnumerator Start()
        //{
        //   yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
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
    }

}

