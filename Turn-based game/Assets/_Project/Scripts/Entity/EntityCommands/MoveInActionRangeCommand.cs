using Assets._Project.Scripts.Entity.Interfaces;
using Assets._Project.Scripts.EntityCommands;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.EntityCommands
{
    public class MoveInActionRangeCommand : ICommand
    {
        public bool IsDone => Vector3.Distance(_targetPosition, _entity.transform.position) <= _neededRange;

        private float _neededRange;
        private Vector2 _targetPosition;

        private GameObject _entity;
        private IMovement _entityMovement;

        private Vector2 _oldPosition;

        public MoveInActionRangeCommand(Vector2 targetPosition, GameObject entity)
        {
            _targetPosition = targetPosition;
            _entity = entity;

            _neededRange = _entity.GetComponent<IWeapon>().AttackRange;
            _oldPosition = _entity.transform.position;
            _entityMovement = _entity.GetComponent<IMovement>();
        }

        public void Execute()
        {
            _entityMovement.MoveToInRange(_targetPosition, _neededRange);
        }

        public void Undo()
        {
            _entity.transform.position = _oldPosition;
        }
    }
}
