using System;
using System.Collections.Generic;
using Models;
using UniRx;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class CharacterController
    {
        private List<CharacterView> _characters = new List<CharacterView>();

        private const float _maxValueTimerInSeconds = 10;

        private const int _pointY = 0;

        private const float _minValuePointZ = -20;
        private const float _minValuePointX = -20;

        private const float _maxValuePointZ = 20;
        private const float _maxValuePointX = 20;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public CharacterController()
        {
            Observable.Timer(TimeSpan.FromSeconds(Random.Range(0, _maxValueTimerInSeconds)))
                .Repeat()
                .Subscribe(_ => CreateCharacter())
                .AddTo(_disposable);
        }

        private void CreateCharacter()
        {
            var randomXPos = Random.Range(_minValuePointX, _maxValuePointX);
            var randomZPos = Random.Range(_minValuePointZ, _maxValuePointZ);

            var randomPosition = new Vector3(randomXPos, _pointY, randomZPos);

            var character = Object.Instantiate(Resources.Load<CharacterView>("Character"), randomPosition,
                Quaternion.identity);
            var characterModel = Object.Instantiate(Resources.Load<Character>("CharacterModel")).characterModel;

            character.Init(characterModel);

            character.takeDamage += TakeDamage;

            _characters.Add(character);
        }

        private void TakeDamage(float damageValue, CharacterView characterView)
        {
            var isDie = characterView.Model.TakeDamageAndCheckDie(damageValue);

            Die(isDie, characterView);
        }

        private void Die(bool isDie, CharacterView characterView)
        {
            if (!isDie) return;

            characterView.takeDamage -= TakeDamage;
            _characters.Remove(characterView);
            Object.Destroy(characterView.gameObject);
        }
    }
}
