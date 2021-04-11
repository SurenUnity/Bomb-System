using System;
using Models;
using UnityEngine;

namespace Views
{
    public class BombView : MonoBehaviour
    {
        public event Action<Collision, BombView> onCollisionEnter;

        public BombModel Model { get; private set; }

        public void Init(BombModel model)
        {
            Model = model;
        }

        private void OnCollisionEnter(Collision other)
        {
            onCollisionEnter?.Invoke(other, this);
        }
    }
}
