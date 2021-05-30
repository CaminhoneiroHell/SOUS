namespace RPG.Stats
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] ECharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public float GetExperience()
        {
            return 10;
        }
    }
}

