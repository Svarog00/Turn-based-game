using System;
using UnityEngine;
using System.Collections.Generic;
using Assets._Project.Scripts.Entity.StateMachine;
using Assets._Project.Scripts.Entity.Interfaces;

namespace Assets._Project.Scripts.Entity
{
    public class EntityBehaviour : MonoBehaviour
    {
        private EntityCommandInvoker _invoker;

        private IMovement _entityMovement;
        private EntityStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new EntityStateMachine();
            _stateMachine.States = new Dictionary<Type, IEntityState>
            {

            };
        }

        public void Initialize()
        {
            _stateMachine = new EntityStateMachine();
        }

        public void Act()
        {
            _stateMachine.Work();
        }
    }
}
