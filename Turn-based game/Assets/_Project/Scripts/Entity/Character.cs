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

        public void SetNewData(CharacterWorldData characterWorldData)
        {
            _entityHealth.HealthPoints = characterWorldData.HealthPoints;
            transform.position = new Vector2(characterWorldData.Position.X, characterWorldData.Position.Y);
        }

        public CharacterWorldData GenerateWorldData()
        {
            return new CharacterWorldData
            {
                Position = new Vector2Surrogate(transform.position),
                HealthPoints = _entityHealth.HealthPoints,
            };
        }
    }
}