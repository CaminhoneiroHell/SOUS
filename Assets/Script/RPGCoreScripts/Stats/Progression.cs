namespace RPG.Stats
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass = null;

        [System.Serializable]
        public class ProgressionCharacterClass
        {
            [SerializeField] ECharacterClass characterClass;
            [SerializeField] float[] health;
        }

    }
}