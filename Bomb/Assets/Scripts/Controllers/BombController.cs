using System;
using System.Collections.Generic;
using Interfaces;
using Models;
using UniRx;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class BombController
    {
        private List<BombView> _bombs = new List<BombView>();

        private const float _maxValueTimerInSeconds = 10;

        private const int _pointY = 10;

        private const float _minValuePointZ = -20;
        private const float _minValuePointX = -20;

        private const float _maxValuePointZ = 20;
        private const float _maxValuePointX = 20;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public BombController()
        {
            Observable.Timer(TimeSpan.FromSeconds(Random.Range(0, _maxValueTimerInSeconds)))
                .Repeat()
                .Subscribe(_ => CreateBomb())
                .AddTo(_disposable);
        }

        private void CreateBomb()
        {
            var randomXPos = Random.Range(_minValuePointX, _maxValuePointX);
            var randomZPos = Random.Range(_minValuePointZ, _maxValuePointZ);

            var randomPosition = new Vector3(randomXPos, _pointY, randomZPos);

            var bomb = Object.Instantiate(Resources.Load<BombView>("Bomb"), randomPosition,
                Quaternion.identity);
            var bombModel = Object.Instantiate(Resources.Load<Bomb>("BombModel")).bombModel;

            bomb.Init(bombModel);

            bomb.onCollisionEnter += OnCollisionEnter;

            _bombs.Add(bomb);
        }

        private void OnCollisionEnter(Collision collider, BombView currentBomb)
        {
            Explode(currentBomb);
        }

        private void Explode(BombView bombView)
        {
            var colliders =
                Physics.OverlapSphere(bombView.transform.position, bombView.Model.radiusDamage);

            foreach (var collider in colliders)
            {
                var character = collider.GetComponent<IDamage>();
                character?.TakeDamage(bombView.Model.damageValue);
            }

            DestroyBomb(bombView);
        }

        private void DestroyBomb(BombView bombView)
        {
            bombView.onCollisionEnter -= OnCollisionEnter;
            _bombs.Remove(bombView);
            Object.Destroy(bombView.gameObject);
        }
    }
}
