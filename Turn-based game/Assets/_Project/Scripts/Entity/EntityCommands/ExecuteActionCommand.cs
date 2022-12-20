using Assets._Project.Scripts.EntityCommands;
using System;

namespace Assets._Project.Scripts.Entity.EntityCommands
{
    public class ExecuteActionCommand : ICommand
    {
        public ExecuteActionCommand()
        {

        }

        public bool IsDone => _isDone;
        private bool _isDone;

        public void Execute()
        {

            _isDone = true;
        }

        public void Undo()
        {
            
        }
    }
}
