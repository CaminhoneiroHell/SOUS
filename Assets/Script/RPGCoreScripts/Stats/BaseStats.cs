﻿namespace RPG.Stats
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int level = 1;
        [SerializeField] ECharacterClass CharacterClass;
        [SerializeField] Progression progression = null;

        public float GetHealth()
        {
            return progression.GetHealth(CharacterClass, level);
        }

        public float GetExperience()
        {
            return 10;
        }
    }
}

