using UnityEngine;

namespace Assets._Project.Scripts.Entity.StateMachine
{
    public class ExecutingCommandState : IEntityState
    {
        private EntityStateMachine _entityStateMachine;
        private ICharacter _character;

        public ExecutingCommandState(EntityStateMachine entityStateMachine, GameObject entityContext)
        {
            _entityStateMachine = entityStateMachine;
            _character = entityContext.GetComponent<ICharacter>();
        }

        public void Enter()
        {
            
        }

        public void Handle()
        {
            if(_character.IsActing)
            {
                return;
            }
            _entityStateMachine.Enter<DecisionMakingState>();
        }

        public void Exit()
        {
            
        }

    }
}
