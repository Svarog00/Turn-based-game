using System;
using UnityEngine;

namespace Assets._Project.Scripts.Entity
{
    public class EntityHealth : MonoBehaviour, IHealth
    {
        public event EventHandler<OnHealthChangedEventArgs> OnHealthChangedEventHandler;

        public int HealthPoints 
        {
            get => _currentHealth; 
            set
            {
                if (value > _maxHealth)
                {
                    return;
                }

                _currentHealth = value;

                gameObject.SetActive(_isAlive);
            }
        }

        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        private bool _isAlive => _currentHealth > 0;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void Heal(int health)
        {
            Debug.Log("Ahh that's better!");
            _currentHealth += health;
            OnHealthChangedEventHandler?.Invoke(this, new OnHealthChangedEventArgs { CurrentHealth = _currentHealth });

            gameObject.SetActive(_isAlive);
        }

        public void Hurt(int damage)
        {
            Debug.Log("Oi that hurts!");
            _currentHealth -= damage;
            OnHealthChangedEventHandler?.Invoke(this, new OnHealthChangedEventArgs { CurrentHealth = _currentHealth });

            gameObject.SetActive(_isAlive);
        }
    }
}
