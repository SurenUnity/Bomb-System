using System;
using UnityEngine;

namespace Views
{
    public class BombView : MonoBehaviour
    {
        public event Action<Collision> onCollisionEnter;

        private void OnCollisionEnter(Collision other)
        {
            onCollisionEnter?.Invoke(other);
        }
    }
}
