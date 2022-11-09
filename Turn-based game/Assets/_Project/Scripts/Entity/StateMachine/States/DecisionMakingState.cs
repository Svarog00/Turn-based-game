namespace Assets._Project.Scripts.Entity.StateMachine
{
    public class DecisionMakingState : IEntityState
    {
        private EntityStateMachine _entityStateMachine;
        private EntityCommandInvoker _commandInvoker;

        public DecisionMakingState(EntityStateMachine stateMachine, EntityCommandInvoker commandInvoker)
        {
            _entityStateMachine = stateMachine;
            _commandInvoker = commandInvoker;
        }

        public void Enter()
        {
            
        }

        public void Handle()
        {
            
        }

        public void Exit()
        {
            
        }

    }
}
