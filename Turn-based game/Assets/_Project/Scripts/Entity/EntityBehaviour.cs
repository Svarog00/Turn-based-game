using System;
using UnityEngine;
using System.Collections.Generic;
using Assets._Project.Scripts.Entity.StateMachine;
using Assets._Project.Scripts.Entity.Interfaces;

namespace Assets._Project.Scripts.Entity
{
    public class EntityBehaviour : MonoBehaviour
    {
        public TurnController TurnController => _turnController;

        [SerializeField] private EntityCommandInvokerInstance _invoker;
        [SerializeField] private TurnController _turnController;

        private EntityStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new EntityStateMachine();
        }

        private void Start()
        {
            _stateMachine.States = new Dictionary<Type, IEntityState>
            {
                [typeof(DecisionMakingState)] = new DecisionMakingState(_stateMachine, _invoker.CommandInvoker, gameObject),
                [typeof(ExecutingCommandState)] = new ExecutingCommandState(_stateMachine, gameObject),
            };

            _stateMachine.Enter<DecisionMakingState>();
        }

        public void StartActing()
        {
            _stateMachine.Enter<DecisionMakingState>();
        }

        public void Act()
        {
            _stateMachine.Work();
        }
    }
}
