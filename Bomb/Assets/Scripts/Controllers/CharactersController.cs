using System;
using System.Collections.Generic;
using Models;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class CharactersController
    {
        private List<CharacterController> _characters = new List<CharacterController>();

        private const float _maxValueTimerInSeconds = 10;

        private const int _pointY = 0;

        private const float _minValuePointZ = -20;
        private const float _minValuePointX = -20;

        private const float _maxValuePointZ = 20;
        private const float _maxValuePointX = 20;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public CharactersController()
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

            var characterModel = Object.Instantiate(Resources.Load<Character>("CharacterModel")).characterModel;
            var character = new CharacterController(characterModel);

            character.CreateCharacter(randomPosition, Quaternion.identity);
            character.dieCharacter += DestroyCharacter;

            _characters.Add(character);
        }

        private void DestroyCharacter(CharacterController characterController)
        {
            characterController.dieCharacter -= DestroyCharacter;
            _characters.Remove(characterController);
        }
    }
}
