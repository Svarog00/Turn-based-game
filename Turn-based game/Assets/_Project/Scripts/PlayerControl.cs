using Assets._Project.Scripts.Entity;
using Assets._Project.Scripts.Entity.EntityCommands;
using Assets._Project.Scripts.EntityCommands;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const string EnemyTagName = "Enemy";
    private const int MouseButtonCode = 1;

    /// <summary>
    /// Считывает действия игрока и создает команды для отправителя в зависимости от контекста
    /// Пример: атака по врагу: если враг далеко, то сначала команда Move до точки на растоянии радиуса атаки, затем Attack
    /// </summary>

    [SerializeField] private EntityCommandInvokerInstance _commandInvokerInstance;
    [SerializeField] private LayerMask _charactersLayer;

    private ICharacter _activeCharacter;
    private GameObject _activeCharacterGameObject;

    public void SetActiveCharacter(GameObject character)
    {
        _activeCharacterGameObject = character;
        _activeCharacter = _activeCharacterGameObject.GetComponent<ICharacter>();
    }

    public void GetInput()
    {
        if(_activeCharacter.IsActing)
        {
            return;
        }

        if (Input.GetMouseButtonDown(MouseButtonCode))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] targetObjects = Physics2D.OverlapCircleAll(mousePosition, 0.1f, _charactersLayer);

            if (targetObjects.Length == 0 
                && _activeCharacter.DistanceCanTravel >= Vector2.Distance(mousePosition, _activeCharacterGameObject.transform.position))
            {
                _commandInvokerInstance.SetCommand(new MoveCommand(_activeCharacterGameObject, 
                    new Vector3(mousePosition.x, mousePosition.y, 0)));
                return;
            }

            if(_activeCharacter.ActionsAvailable > 0)
            {
                foreach (Collider2D targetObject in targetObjects)
                {
                    if(targetObject.tag != EnemyTagName)
                    {
                        continue;
                    }

                    if (Vector2.Distance(targetObject.transform.position, _activeCharacterGameObject.transform.position) <= 
                        _activeCharacterGameObject.GetComponent<IWeapon>().AttackRange)
                    {
                        _commandInvokerInstance.SetCommand(new AttackCommand(targetObject.gameObject, _activeCharacterGameObject));
                        return;
                    }

                    if(Vector2.Distance(targetObject.transform.position, _activeCharacterGameObject.transform.position) <= _activeCharacter.DistanceCanTravel)
                    {
                        MacroCommand attackMacroCommand = new MacroCommand();
                        attackMacroCommand.AddCommand(new MoveInActionRangeCommand(new Vector3(mousePosition.x, mousePosition.y, 0),
                            _activeCharacterGameObject));
                        attackMacroCommand.AddCommand(new AttackCommand(targetObject.gameObject, _activeCharacterGameObject));

                        _commandInvokerInstance.SetCommand(attackMacroCommand);
                        return;
                    }
                }
            }
        }
    }
}
