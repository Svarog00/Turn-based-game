using Assets._Project.Scripts.Entity.Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.EntityCommands
{
    public class MoveCommand : IContinousCommand
    {
        public bool IsDone => Vector3.Distance(_targetPosition, _entityTransform.position) <= 0.5f;

        private IMovement _entityMovement;
        private ICharacter _character;
        
        private Transform _entityTransform;

        private Vector3 _targetPosition;
        private Vector2 _oldPosition;

        public MoveCommand(GameObject entity, Vector3 targetPosition)
        {
            _entityMovement = entity.GetComponent<IMovement>();
            _character = entity.GetComponent<ICharacter>();

            _targetPosition = targetPosition;
            _entityTransform = entity.transform;

            _oldPosition = _entityTransform.position;
        }

        public void Execute()
        {
            _entityMovement.MoveTo(_targetPosition);
            _character.IsActing = true;
            WaitCommandDone();
        }

        public void Cancel()
        {
            _entityMovement.StopMove();

            //_character.DistanceCanTravel -= Vector2.Distance(_oldPosition, _targetPosition);
            _character.ActionsAvailable--;
            _character.IsActing = false;
        }

        public void Undo()
        {
            //_character.DistanceCanTravel += Vector2.Distance(_oldPosition, _targetPosition);
            _character.ActionsAvailable++;
            _entityTransform.position = _oldPosition;
        }

        public void WaitCommandDone()
        {
            WaitCommandDoneAsync();
        }

        private async Task WaitCommandDoneAsync()
        {
            while(!IsDone)
            {
                Debug.Log(Vector3.Distance(_targetPosition, _entityTransform.position));
                await Task.Yield();
            }

            //_character.DistanceCanTravel -= Vector2.Distance(_oldPosition, _targetPosition);
            _character.ActionsAvailable--;
            _character.IsActing = false;
        }
    }
}
