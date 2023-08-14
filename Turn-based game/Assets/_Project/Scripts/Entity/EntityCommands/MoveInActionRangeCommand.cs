using Assets._Project.Scripts.Entity.Interfaces;
using Assets._Project.Scripts.EntityCommands;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.EntityCommands
{
    public class MoveInActionRangeCommand : IContinousCommand
    {
        public bool IsDone => Vector3.Distance(_targetPosition, _entity.transform.position) <= _neededRange;

        private float _neededRange;

        private GameObject _entity;
        private IMovement _entityMovement;
        private ICharacter _character;

        private Vector2 _targetPosition;
        private Vector2 _oldPosition;

        public MoveInActionRangeCommand(GameObject executioner, Vector2 targetPosition, float neededRange)
        {
            _targetPosition = targetPosition;
            _entity = executioner;

            _entityMovement = _entity.GetComponent<IMovement>();
            _character = _entity.GetComponent<ICharacter>();

            _neededRange = neededRange;
            _oldPosition = _entity.transform.position;
        }

        public void Execute()
        {
            _character.IsActing = true;
            _entityMovement.MoveTo(_targetPosition);
            WaitCommandDone();
        }

        public void Cancel()
        {
            //_character.DistanceCanTravel -= Vector2.Distance(_oldPosition, _targetPosition);
            _character.ActionsAvailable--;
            _character.IsActing = false;
            _entityMovement.StopMove();
        }

        public void Undo()
        {
            //_character.DistanceCanTravel += Vector2.Distance(_oldPosition, _targetPosition);
            _character.ActionsAvailable++;
            _entity.transform.position = _oldPosition;
        }

        public void WaitCommandDone()
        {
            WaitCommandDoneAsync();
        }

        private async Task WaitCommandDoneAsync()
        {
            while (!IsDone)
            {
                await Task.Yield();
            }

            //_character.DistanceCanTravel -= Vector2.Distance(_oldPosition, _targetPosition);
            _character.ActionsAvailable--;
            _character.IsActing = false;
            _entityMovement.StopMove();
        }
    }
}
