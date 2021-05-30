namespace RPG.Stats
{
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using RPG.Saving;
    using RPG.Stats;
    using RPG.Core;

    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", baseStats.GetLevel());
        }
    }
}
