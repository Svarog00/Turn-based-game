using Assets._Project.Scripts.Entity;
using Assets._Project.Scripts.Entity.Actions;
using Assets._Project.Scripts.Entity.EntityCommands;
using Assets._Project.Scripts.Entity.Interfaces;
using Assets._Project.Scripts.EntityCommands;
using Services;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Считывает действия игрока и создает команды для отправителя в зависимости от контекста
    /// Пример: атака по врагу: если враг далеко, то сначала команда Move до точки на растоянии радиуса атаки, затем Attack
    /// </summary>

    [SerializeField] private EntityCommandInvokerInstance _commandInvokerInstance;
    [SerializeField] private LayerMask _charactersLayer;

    private IAction _selectedAction;

    private ICharacter _activeCharacter;
    private GameObject _activeCharacterGameObject;
    private IClickHandlerService _clickHandlerService;

    public void Awake()
    {
        _clickHandlerService = new ClickHandlerService(_charactersLayer);
    }

    public void SetActiveCharacter(GameObject character)
    {
        _activeCharacterGameObject = character;
        _activeCharacter = _activeCharacterGameObject.GetComponent<ICharacter>();
        _selectedAction = new AttackAction(_activeCharacterGameObject.GetComponent<IWeapon>());
    }

    public void ChangeSelectedAction(IAction newAction)
    {
        _selectedAction = newAction;
    }

    public void GetInput()
    {
        if(_activeCharacter.IsActing)
        {
            return;
        }

        if (_activeCharacter.ActionsAvailable == 0)
        {
            return;
        }

        ProcessLeftClick();
    }

    private void ProcessLeftClick()
    {
        Vector3 mousePosition;

        if (_clickHandlerService.IsLeftButtonDown(out mousePosition))
        {
            GameObject targetObject = _clickHandlerService.GetObjectFromClick();

            if (targetObject == null)
            {
                if (_activeCharacter.DistanceCanTravel <= Vector2.Distance(mousePosition, _activeCharacterGameObject.transform.position))
                {
                    return;
                }

                _commandInvokerInstance.SetCommand(new MoveCommand(_activeCharacterGameObject,
                        new Vector3(mousePosition.x, mousePosition.y, 0)));

                return;
            }

            float distanceToTarget = Vector2.Distance(targetObject.transform.position, _activeCharacterGameObject.transform.position);

            if (distanceToTarget <= _selectedAction.ActionRange)
            {
                _commandInvokerInstance.SetCommand(new AttackCommand(targetObject.gameObject, _activeCharacterGameObject));
                return;
            }

            if (distanceToTarget <= _activeCharacter.DistanceCanTravel)
            {
                MacroCommand attackMacroCommand = new MacroCommand();
                attackMacroCommand.AddCommand(new MoveInActionRangeCommand(
                    _activeCharacterGameObject,
                    new Vector3(mousePosition.x, mousePosition.y, 0),
                    _selectedAction.ActionRange));

                attackMacroCommand.AddCommand(new AttackCommand(targetObject.gameObject, _activeCharacterGameObject));
                //attackMacroCommand.AddCommand(new ExecuteActionCommand(_selectedAction, _activeCharacter));

                _commandInvokerInstance.SetCommand(attackMacroCommand);
                return;
            }
        }
    }
}


