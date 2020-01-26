using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class Aim : MonoBehaviour, ISetDamage, ISelectObj
    {
        public event Action OnPointChange;
		
        public float Hp = 100;
        public float Armor = 0;
        public float Defense = 0;

        private bool _isDead;
        //todo дописать поглащение урона
        public void SetDamage(InfoCollision info)
        {
            float damage = info.Damage / Armor;

            if (_isDead) return;

            if (Defense > 0)
            {
                Defense -= damage;
            }
            else if (Hp > 0)
            {
                Hp -= damage;
            }

            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, 10);

                OnPointChange?.Invoke();
                _isDead = true;
            }
        }

        public string GetMessage()
        {
            return gameObject.name;
        }
    }
}
