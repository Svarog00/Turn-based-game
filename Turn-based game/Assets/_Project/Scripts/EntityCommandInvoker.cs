using Assets._Project.Scripts.EntityCommands;
using System;
using System.Collections.Generic;


namespace Assets._Project.Scripts
{
    public class EntityCommandInvoker
    {
        public List<ICommand> ExecutedCommands
        {
            get => _undoCommandsQueue;
            set => _undoCommandsQueue = value;
        }
        private List<ICommand> _undoCommandsQueue;

        private int _currentCommandNumber = 0;

        public EntityCommandInvoker()
        {
            _undoCommandsQueue = new List<ICommand>();
        }

        public void SetCommand(ICommand command)
        {
            _undoCommandsQueue.Add(command);
            _currentCommandNumber = _undoCommandsQueue.Count-1;
            command.Execute();
        }

        public void UndoCommand()
        {
            if(_currentCommandNumber > -1)
            {
                ICommand commandToUndo = _undoCommandsQueue[_currentCommandNumber];
                commandToUndo.Undo();
                _currentCommandNumber--;
            }
        }
    }
}
