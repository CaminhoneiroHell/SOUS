using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.DevelopmentSettings
{
    public class DevSetting : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {

                Time.timeScale = 2f;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {

                Time.timeScale = 10;
            }
        }
    }

}
