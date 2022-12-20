using Assets._Project.Scripts.Entity.Interfaces;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.Actions
{
    public class PushAction : IAction
    {
        public float ActionRange => 0.4f;

        public void ExecuteAction(GameObject target)
        {
            //target.GetComponent<Rigidbody2D>().;
        }
    }
}
