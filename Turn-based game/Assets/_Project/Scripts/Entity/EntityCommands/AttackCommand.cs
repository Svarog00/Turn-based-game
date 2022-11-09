using Assets._Project.Scripts.Entity;
using UnityEngine;

namespace Assets._Project.Scripts.EntityCommands
{
    public class AttackCommand : ICommand
    {
        public bool IsDone => _isDone;

        private GameObject _attackTarget;
        private GameObject _reciever;

        private bool _isDone;

        public AttackCommand(GameObject target, GameObject executioner)
        {
            _attackTarget = target;
            _reciever = executioner;

            _isDone = false;
        }

        public void Execute()
        {
            _attackTarget.GetComponent<IHealth>().Hurt(_reciever.GetComponent<IWeapon>().Damage);
            _isDone = true;
        }

        public void Undo()
        {
            _attackTarget.GetComponent<IHealth>().Heal(_reciever.GetComponent<IWeapon>().Damage);
        }
    }
}
