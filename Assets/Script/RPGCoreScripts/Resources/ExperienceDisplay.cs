

namespace RPG.Resources
{
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using RPG.Saving;
    using RPG.Stats;
    using RPG.Core;

    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", experience.GetPoints());
        }
    }
}
