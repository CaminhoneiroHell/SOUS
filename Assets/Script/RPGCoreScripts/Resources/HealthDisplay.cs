

namespace RPG.Resources
{
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using RPG.Saving;
    using RPG.Stats;
    using RPG.Core;

    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            //GetComponent<Text>().text = string.Format("{0:0}%", health.GetPercentage(), "%"); //Percentage
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
