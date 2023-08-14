using Assets._Project.Scripts.Entity.Interfaces;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.Actions
{
    public class PushAction : IAction
    {
        public float ActionRange => 0.4f;

        private GameObject _target;
        private GameObject _executor;

        private float _pushForce = 0.5f;

        public PushAction(GameObject target, GameObject executor)
        {
            _target = target;
            _executor = executor;
        }

        public void ExecuteAction()
        {
            var vectorToTarget = _target.transform.position - _executor.transform.position;

            _target.GetComponent<Rigidbody2D>().AddForce(-vectorToTarget.normalized * _pushForce);
        }
    }
}
