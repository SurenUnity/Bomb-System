using System;
using Interfaces;
using Models;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class BombController
    {
        public event Action<BombController> destroyBomb;

        private BombView _bombView;
        private BombModel _model;

        public BombController(BombModel bombModel)
        {
            _model = bombModel;
        }

        public void CreateBomb(Vector3 position, Quaternion rotation)
        {
            _bombView = Object.Instantiate(Resources.Load<BombView>("Bomb"), position,
                rotation);

            _bombView.onCollisionEnter += OnCollisionEnter;
        }

        private void OnCollisionEnter(Collision collider)
        {
            Explode();
        }

        private void Explode()
        {
            var colliders =
                Physics.OverlapSphere(_bombView.transform.position, _model.radiusDamage);

            foreach (var collider in colliders)
            {
                var character = collider.GetComponent<IDamage>();
                character?.TakeDamage(_model.damageValue);
            }

            DestroyBomb();
        }

        private void DestroyBomb()
        {
            _bombView.onCollisionEnter -= OnCollisionEnter;
            Object.Destroy(_bombView.gameObject);
            destroyBomb?.Invoke(this);
        }
    }
}
