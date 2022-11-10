using Assets._Project.Scripts.EntityCommands;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Assets._Project.Scripts.Entity.EntityCommands
{
    public class MacroCommand : ICommand
    {
        public bool IsDone { get; private set; }

        private List<ICommand> _commands;
        private ICommand _currentCommand;

        public MacroCommand()
        {
            _commands = new List<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void Execute()
        {
            ExecuteCommandsAsync();
        }

        public void Undo()
        {
            for (int i = _commands.Count-1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }

        private async void ExecuteCommandsAsync()
        {
            for(int i = 0; i < _commands.Count; i++)
            {
                _currentCommand = _commands[i];
                _currentCommand.Execute();
                while(!_currentCommand.IsDone)
                {
                    await Task.Yield();
                }
            }

            IsDone = true;
        }
    }
}
