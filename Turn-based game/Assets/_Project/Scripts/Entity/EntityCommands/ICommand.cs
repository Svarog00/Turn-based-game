using System.Threading.Tasks;

namespace Assets._Project.Scripts.EntityCommands
{
    public interface ICommand
    {
        public bool IsDone { get; }

        public void Execute();
        public void Cancel();
        public void Undo();

    }

    public interface IContinousCommand : ICommand
    {
        void WaitCommandDone();
    }
}