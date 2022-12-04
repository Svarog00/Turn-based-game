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
        public int ActionsAvailable { get; set; }
        public float DistanceCanTravel { get; set; }

        [SerializeField] private Turn _side;
        [SerializeField] private int _maxActionsAvailable;
        [SerializeField] private float _maxDistanceCanTravel;

        private EntityHealth _entityHealth;

        private void Awake()
        {
            _entityHealth = GetComponent<EntityHealth>();

            DistanceCanTravel = _maxDistanceCanTravel;
            ActionsAvailable = _maxActionsAvailable;
        }

        public void ResetTurn()
        {
            DistanceCanTravel = _maxDistanceCanTravel;
            ActionsAvailable = _maxActionsAvailable;
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