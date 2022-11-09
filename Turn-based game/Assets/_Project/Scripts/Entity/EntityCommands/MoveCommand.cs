using Assets._Project.Scripts.Entity.Interfaces;
using UnityEngine;

namespace Assets._Project.Scripts.EntityCommands
{
    public class MoveCommand : ICommand
    {
        public bool IsDone => Vector3.Distance(_targetPosition, _entityTransform.position) <= 0.3f;

        private IMovement _entityMovement;

        private Transform _entityTransform;

        private Vector3 _targetPosition;
        private Vector2 _oldPosition;


        public MoveCommand(GameObject entity, Vector3 targetPosition)
        {
            _entityMovement = entity.GetComponent<IMovement>();
            _targetPosition = targetPosition;
            _entityTransform = entity.transform;

            _oldPosition = _entityTransform.position;
        }

        public void Execute()
        {
            _entityMovement.MoveTo(_targetPosition);
        }

        public void Undo()
        {
            _entityTransform.position = _oldPosition;
        }
    }
}
