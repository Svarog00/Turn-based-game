using Assets._Project.Scripts.Entity.EntityCommands;
using Assets._Project.Scripts.EntityCommands;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const string EnemyTagName = "Enemy";

    /// <summary>
    /// Считывает действия игрока и создает команды для отправителя в зависимости от контекста
    /// Пример: атака по врагу: если враг далеко, то сначала команда Move до точки на растоянии радиуса атаки, затем Attack
    /// </summary>

    [SerializeField] private EntityCommandInvokerInstance _commandInvokerInstance;
    [SerializeField] private LayerMask _charactersLayer;

    private GameObject _activeCharacter;
    private ICharacter _activeCharacterRoot;

    public void SetActiveCharacter(GameObject character)
    {
        _activeCharacter = character;
    }

    public void GetInput()
    {
        /*if(_activeCharacterRoot.IsActing)
        {
            return;
        }*/

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] targetObjects = Physics2D.OverlapCircleAll(mousePosition, 0.1f, _charactersLayer);

            if (targetObjects.Length == 0)
            {
                _commandInvokerInstance.SetCommand(new MoveCommand(_activeCharacter, 
                    new Vector3(mousePosition.x, mousePosition.y, 0)));
                return;
            }

            foreach (Collider2D targetObject in targetObjects)
            {
                if(targetObject.tag == EnemyTagName)
                {
                    MacroCommand macroCommand = new MacroCommand();
                    macroCommand.AddCommand(new MoveInActionRangeCommand(new Vector3(mousePosition.x, mousePosition.y, 0), 
                        _activeCharacter));
                    macroCommand.AddCommand(new AttackCommand(targetObject.gameObject, _activeCharacter));

                    _commandInvokerInstance.SetCommand(macroCommand);
                    return;
                }
            }
        }
    }
}
