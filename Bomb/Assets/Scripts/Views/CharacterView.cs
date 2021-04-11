using System;
using Interfaces;
using Models;
using UnityEngine;

namespace Views
{
    public class CharacterView : MonoBehaviour, IDamage
    {
        public event Action<float, CharacterView> takeDamage;

        public CharacterModel Model { get; private set; }

        public void Init(CharacterModel model)
        {
            Model = model;
        }

        public void TakeDamage(float value)
        {
            takeDamage?.Invoke(value, this);
        }
    }
}
