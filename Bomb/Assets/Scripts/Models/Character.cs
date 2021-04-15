using System;
using UniRx;
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
        public ReactiveProperty<int> health;

        public CharacterModel()
        {
            health = new ReactiveProperty<int>();
        }
    }
}
