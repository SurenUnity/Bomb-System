using System;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Character", menuName = "Characters", order = 0)]
    public class Character : ScriptableObject
    {
        public CharacterModel characterModel;
    }

    [Serializable]
    public class CharacterModel
    {
        public int health;

        public bool TakeDamageAndCheckDie(float damageValue)
        {
            return health - damageValue <= 0;
        }
    }
}
