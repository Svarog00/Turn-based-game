using UnityEngine;
using Assets._Project.Scripts.Data;

namespace Assets._Project.Scripts.Entity
{
    public class Character : MonoBehaviour, ICharacter
    {
        public Turn Side
        {
            get => _side;
        }
        public bool IsActing { get; set; }

        [SerializeField] private Turn _side;
        private EntityHealth _entityHealth;

        private void Awake()
        {
            _entityHealth = GetComponent<EntityHealth>();
        }

        public CharacterWorldData GenerateWorldData()
        {
            return new CharacterWorldData
            {
                Position = new Vector2Surrogate(transform.position),

                HealthPoints = gameObject.GetComponent<IHealth>().HealthPoints,
            };
        }
    }
}