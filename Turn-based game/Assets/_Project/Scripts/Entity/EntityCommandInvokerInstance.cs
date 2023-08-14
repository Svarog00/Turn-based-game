using Assets._Project.Scripts;
using Assets._Project.Scripts.EntityCommands;
using UnityEngine;

public class EntityCommandInvokerInstance : MonoBehaviour
{
    public EntityCommandInvoker CommandInvoker => _commandInvoker;

    private EntityCommandInvoker _commandInvoker;

    private void Awake()
    {
        _commandInvoker = new EntityCommandInvoker();
    }

    public void SetCommand(ICommand command)
    {
        _commandInvoker.SetCommand(command);
    }

    public void UndoCommand()
    {
        _commandInvoker.UndoCommand();
    }
}
