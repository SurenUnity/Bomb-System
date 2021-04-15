using System;
using Models;
using UniRx;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class CharacterController
    {
        public event Action<CharacterController> dieCharacter;

        private CharacterView _character;

        private CharacterModel _model;

        public CharacterController(CharacterModel model)
        {
            _model = model;
            _model.health
                .ObserveEveryValueChanged(h => h.Value)
                .Subscribe(ChangeHp);
        }

        public void CreateCharacter(Vector3 position, Quaternion rotation)
        {
            _character = Object.Instantiate(Resources.Load<CharacterView>("Character"), position,
                rotation);

            _character.takeDamage += TakeDamage;
        }

        private void TakeDamage(int damageValue)
        {
            _model.health.Value -= damageValue;

            if (_model.health.Value <= 0)
            {
                Die();
            }
        }

        private void ChangeHp(int hpValue)
        {
            // Меняем визуал Вью
        }

        private void Die()
        {
            _character.takeDamage -= TakeDamage;
            Object.Destroy(_character.gameObject);
            dieCharacter?.Invoke(this);
        }
    }
}
