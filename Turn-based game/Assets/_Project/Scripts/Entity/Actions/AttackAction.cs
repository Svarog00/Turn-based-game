using Assets._Project.Scripts.Entity.Interfaces;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.Actions
{
    public class AttackAction : IAction
    {
        public float ActionRange => _weapon.AttackRange;

        private IWeapon _weapon;
        private GameObject _target;

        public AttackAction(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public AttackAction(IWeapon weapon, GameObject target)
        {
            _weapon = weapon;
            _target = target;
        }

        public void ExecuteAction()
        {
            _weapon.Attack(_target.GetComponent<IHealth>());
        }
    }
}
