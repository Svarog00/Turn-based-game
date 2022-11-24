using Assets._Project.Scripts.Entity.EntityCommands;
using Assets._Project.Scripts.EntityCommands;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const string EnemyTagName = "Enemy";
    private const int LeftMouseButtonCode = 1;

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
        _activeCharacterRoot = _activeCharacter.GetComponent<ICharacter>();
    }

    public void GetInput()
    {
        if(_activeCharacterRoot.IsActing)
        {
            return;
        }

        if (Input.GetMouseButtonDown(LeftMouseButtonCode))
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
                    MacroCommand attackMacroCommand = new MacroCommand();
                    attackMacroCommand.AddCommand(new MoveInActionRangeCommand(new Vector3(mousePosition.x, mousePosition.y, 0), 
                        _activeCharacter));
                    attackMacroCommand.AddCommand(new AttackCommand(targetObject.gameObject, _activeCharacter));

                    _commandInvokerInstance.SetCommand(attackMacroCommand);
                    return;
                }
            }
        }
    }
}
