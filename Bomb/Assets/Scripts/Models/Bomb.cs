using System;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Bomb", menuName = "Bombs", order = 0)]
    public class Bomb : ScriptableObject
    {
        public BombModel bombModel;
    }

    [Serializable]
    public class BombModel
    {
        public float radiusDamage;
        public int damageValue;
    }
}
