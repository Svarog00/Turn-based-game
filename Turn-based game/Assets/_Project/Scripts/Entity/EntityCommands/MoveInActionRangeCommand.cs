using Assets._Project.Scripts.Entity.Interfaces;
using Assets._Project.Scripts.EntityCommands;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.EntityCommands
{
    public class MoveInActionRangeCommand : ICommand
    {
        public bool IsDone => Vector3.Distance(_targetPosition, _entity.transform.position) <= _neededRange;

        private float _neededRange;

        private GameObject _entity;
        private IMovement _entityMovement;
        private ICharacter _character;

        private Vector2 _oldPosition;
        private Vector2 _targetPosition;

        public MoveInActionRangeCommand(Vector2 targetPosition, GameObject entity)
        {
            _targetPosition = targetPosition;
            _entity = entity;

            _entityMovement = _entity.GetComponent<IMovement>();
            _character = _entity.GetComponent<ICharacter>();

            _neededRange = _entity.GetComponent<IWeapon>().AttackRange;
            _oldPosition = _entity.transform.position;
        }

        public void Execute()
        {
            _character.IsActing = true;
            _entityMovement.MoveToInRange(_targetPosition, _neededRange);
            WaitCommandDone();
        }

        public void Undo()
        {
            _character.DistanceCanTravel += Vector2.Distance(_oldPosition, _targetPosition);
            _entity.transform.position = _oldPosition;
        }

        private async void WaitCommandDone()
        {
            while (!IsDone)
            {
                await Task.Yield();
            }

            _character.DistanceCanTravel -= Vector2.Distance(_oldPosition, _targetPosition);
            _character.IsActing = false;
        }
    }
}
