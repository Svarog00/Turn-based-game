using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Scripts.Entity.StateMachine
{
    public class EntityStateMachine
    {
        public IEntityState CurrentState => _currentState;
        public Dictionary<Type, IEntityState> States
        {
            get => _states;
            set => _states = value;
        }

        private Dictionary<Type, IEntityState> _states;

        private IEntityState _currentState;

        public void Enter<TState>() where TState : class, IEntityState
        {
            _currentState = ChangeState<TState>();
            _currentState.Enter();
        }

        public void Work()
        {
            _currentState?.Handle();
        }

        public TState ChangeState<TState>() where TState : class, IEntityState
        {
            _currentState?.Exit();
            return _states[typeof(TState)] as TState;
        }
    }
}
