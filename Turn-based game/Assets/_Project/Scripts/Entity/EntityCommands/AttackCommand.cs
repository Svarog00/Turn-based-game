using Assets._Project.Scripts.Entity;
using UnityEngine;

namespace Assets._Project.Scripts.EntityCommands
{
    public class AttackCommand : ICommand
    {
        public bool IsDone => _isDone;

        private GameObject _attackTarget;
        private GameObject _reciever;

        private ICharacter _character;
        private IWeapon _weapon;

        private bool _isDone;

        public AttackCommand(GameObject target, GameObject executioner)
        {
            _attackTarget = target;
            _reciever = executioner;

            _character = _reciever.GetComponent<Character>();
            _weapon = _reciever.GetComponent<IWeapon>();

            _isDone = false;
        }

        public void Execute()
        {
            _weapon.Attack(_attackTarget.GetComponent<IHealth>());

            _character.ActionsAvailable--;
            _isDone = true;
        }

        public void Cancel()
        {
            
        }

        public void Undo()
        {
            _character.ActionsAvailable++;
            _attackTarget.GetComponent<IHealth>().Heal(_weapon.Damage);
        }
    }
}
