using Assets._Project.Scripts.Entity.EntityCommands;
using Assets._Project.Scripts.EntityCommands;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.StateMachine
{
    public class DecisionMakingState : IEntityState
    {
        private EntityStateMachine _entityStateMachine;
        private EntityCommandInvoker _commandInvoker;
        private TurnController _turnController;
        private GameObject _entityGameObject;

        private IWeapon _weapon;
        private ICharacter _character;

        private IHealth _health;

        private GameObject _target;
        private Transform _entityTransform;

        private float _distanceToNearestEnemy;

        public DecisionMakingState(EntityStateMachine stateMachine, EntityCommandInvoker commandInvoker, GameObject entityContext)
        {
            _entityStateMachine = stateMachine;
            _commandInvoker = commandInvoker;
            _entityGameObject = entityContext;

            _entityTransform = _entityGameObject.transform;

            _weapon = _entityGameObject.GetComponent<IWeapon>();
            _character = _entityGameObject.GetComponent<ICharacter>();
            _health = _entityGameObject.GetComponent<IHealth>();

            _turnController = _entityGameObject.GetComponent<EntityBehaviour>().TurnController;
        }

        public void Enter()
        {
            ChooseTarget();
        }

        /// <summary>
        /// Выбираем ближайшую цель, атакуем, но
        /// Если здоровье меньше, но рядом (_friendSupportRadius) есть други, то бьем цель
        /// Если здоровье меньше порогового, то отступаем строго в противоположную сторону (векторДвижения = вектор с длиной радиуса атаки * -направление к противнику)
        /// Атака:
        ///     Если дистанция меньше или равна дистанции атаки, то просто создаем команду атаки
        ///     Если дистанция больше, то макрокоманда
        /// </summary>

        public void Handle()
        {
            if (_character.ActionsAvailable <= 0)
            {
                PassTurn();
                return;
            }

            if(_health.HealthPoints > 25)
            {
                AttackTarget();
            }

            if(_health.HealthPoints < 25 && !IsAlone())
            {
                AttackTarget();
            }

            if (_health.HealthPoints < 25 && IsAlone() && _distanceToNearestEnemy < _weapon.AttackRange)
            {
                Flee();
            }

            _entityStateMachine.Enter<ExecutingCommandState>();
        }

        public void Exit()
        {
            
        }

        private void ChooseTarget()
        {
            var charactersAround = GameObject.FindObjectsOfType<Character>().Where(character => character.Side != _character.Side).ToArray();

            _distanceToNearestEnemy = float.MaxValue;
            foreach(var character in charactersAround)
            {
                if(Vector2.Distance(_entityTransform.position, character.transform.position) <= _distanceToNearestEnemy)
                {
                    _target = character.gameObject;
                }
            }
        }

        private bool IsAlone()
        {
            var charactersAround = GameObject.FindObjectsOfType<Character>().Where(character => character.Side == _character.Side);
            return charactersAround.Count() == 0;
        }

        private void AttackTarget()
        {
            if (_distanceToNearestEnemy <= _weapon.AttackRange)
            {
                _commandInvoker.SetCommand(new AttackCommand(_target, _entityGameObject));
                return;
            }

            if (_distanceToNearestEnemy > _weapon.AttackRange)
            {
                MacroCommand attackMacroCommand = new MacroCommand();
                attackMacroCommand.AddCommand(
                    new MoveInActionRangeCommand(
                        _entityGameObject,
                        new Vector3(_target.transform.position.x, _target.transform.position.y, 0),
                        _weapon.AttackRange));

                attackMacroCommand.AddCommand(new AttackCommand(_target, _entityGameObject));

                _commandInvoker.SetCommand(attackMacroCommand);

                return;
            }
        }

        private void Flee()
        {
            Vector3 direction = (_target.transform.position - _entityTransform.position).normalized;
            Vector3 targetPosition = _entityTransform.position + _weapon.AttackRange * -direction;

            _commandInvoker.SetCommand(
                new MoveCommand(_entityGameObject, targetPosition));
        }

        private void PassTurn()
        {
            _turnController.EndTurn();
        }
    }
}
