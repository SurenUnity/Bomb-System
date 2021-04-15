using System;
using System.Collections.Generic;
using Models;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class BombsController
    {
        private List<BombController> _bombs = new List<BombController>();

        private const float _maxValueTimerInSeconds = 10;

        private const int _pointY = 10;

        private const float _minValuePointZ = -20;
        private const float _minValuePointX = -20;

        private const float _maxValuePointZ = 20;
        private const float _maxValuePointX = 20;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public BombsController()
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

            var bombModel = Object.Instantiate(Resources.Load<Bomb>("BombModel")).bombModel;
            var bomb = new BombController(bombModel);
            bomb.destroyBomb += DestroyBomb;

            bomb.CreateBomb(randomPosition, Quaternion.identity);

            _bombs.Add(bomb);
        }

        private void DestroyBomb(BombController bombController)
        {
            bombController.destroyBomb -= DestroyBomb;
            _bombs.Remove(bombController);
        }
    }
}
