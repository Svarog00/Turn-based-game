using Assets._Project.Scripts.Entity.Interfaces;
using Assets._Project.Scripts.EntityCommands;
using System;

namespace Assets._Project.Scripts.Entity.EntityCommands
{
    public class ExecuteActionCommand : ICommand
    {
        private IAction _action;
        private ICharacter _character;

        public ExecuteActionCommand(IAction actionToExecute, ICharacter executioner)
        {
            _action = actionToExecute;
            _character = executioner;
        }

        public bool IsDone => _isDone;

        private bool _isDone;

        public void Execute()
        {
            _action.ExecuteAction();
            _character.ActionsAvailable--;
            _isDone = true;
        }

        public void Cancel()
        {
            _isDone = true;
        }

        public void Undo()
        {
            
        }
    }
}
