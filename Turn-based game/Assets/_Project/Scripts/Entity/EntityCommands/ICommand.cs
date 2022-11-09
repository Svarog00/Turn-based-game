namespace Assets._Project.Scripts.EntityCommands
{
    public interface ICommand
    {
        public bool IsDone { get; }

        public void Execute();
        public void Undo();
    }
}