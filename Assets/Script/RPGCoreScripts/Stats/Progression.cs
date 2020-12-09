namespace RPG.Stats
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass = null;

        public float GetHealth(ECharacterClass charClassEnum, int level)
        {
            foreach(ProgressionCharacterClass progressionCharacter in characterClass)
            {
                if(progressionCharacter.characterClass == charClassEnum)
                {
                    return progressionCharacter.health[level - 1];
                }
            }

            return 1;
        }

        [System.Serializable]
        public class ProgressionCharacterClass
        {
            public ECharacterClass characterClass;
            public  float[] health;
        }

    }
}