using System;
using UnityEngine;

namespace Assets._Project.Scripts.Entity
{
    public class EntityHealth : MonoBehaviour, IHealth
    {
        public event EventHandler<HealthChangedEventArgs> OnHealthChanged;

        public class HealthChangedEventArgs
        {
            public int Health { get; set; }
        }

        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        public int HealthPoints => _currentHealth;

        public void Heal(int health)
        {
            Debug.Log("Ahh that's better!");
            _currentHealth += health;
            OnHealthChanged?.Invoke(this, new HealthChangedEventArgs { Health = _currentHealth });
        }

        public void Hurt(int damage)
        {
            Debug.Log("Oi that hurts!");
            _currentHealth -= damage;
            OnHealthChanged?.Invoke(this, new HealthChangedEventArgs { Health = _currentHealth });
        }
    }
}
