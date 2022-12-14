using Assets._Project.Scripts.Entity.Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.EntityCommands
{
    public class MoveCommand : ICommand
    {
        public bool IsDone => Vector3.Distance(_targetPosition, _entityTransform.position) <= 0.4f;

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
            WaitCommandDoneAsync();
        }

        public void Undo()
        {
            _character.DistanceCanTravel += Vector2.Distance(_oldPosition, _targetPosition);
            _entityTransform.position = _oldPosition;
        }

        private async void WaitCommandDoneAsync()
        {
            while(!IsDone)
            {
                await Task.Yield();
            }
            _character.DistanceCanTravel -= Vector2.Distance(_oldPosition, _targetPosition);
            _character.IsActing = false;
        }
    }
}
