using Assets._Project.Scripts.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.Actions
{
    public class AttackAction : IAction
    {
        public float ActionRange => Weapon.AttackRange;

        public IWeapon Weapon;

        public void ExecuteAction(GameObject target)
        {
            Weapon.Attack(target.GetComponent<IHealth>());
        }
    }
}
