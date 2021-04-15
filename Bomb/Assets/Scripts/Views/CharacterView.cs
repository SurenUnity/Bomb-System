using System;
using Interfaces;
using UnityEngine;

namespace Views
{
    public class CharacterView : MonoBehaviour, IDamage
    {
        public event Action<int> takeDamage;

        public void TakeDamage(int value)
        {
            takeDamage?.Invoke(value);
        }
    }
}
