namespace RPG.Stats
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass = null;

        public float GetStat(Stat stat, ECharacterClass characterClasses, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClass)
            {
                if (progressionClass.characterClass != characterClasses) continue;

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if (progressionStat.stat != stat) continue;

                    if (progressionStat.levels.Length < level) continue;

                    return progressionStat.levels[level - 1];
                }
            }
            return 0;
        }

        [System.Serializable]
        public class ProgressionCharacterClass
        {
            public ECharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        public class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}