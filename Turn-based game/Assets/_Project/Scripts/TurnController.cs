using Assets._Project.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn 
{
    Player, 
    Npc,
}

public class TurnController : MonoBehaviour
{
    public IEnumerable<GameObject> Characters => _charactersOrder;
    public int CurrentCharacterIndex;

    [SerializeField] private GameObject _startCharacter;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private List<GameObject> _charactersOrder;

    private Turn _turn;
    private int _currentCharacterIndex;
    private EntityBehaviour _currentNpc;

    void Start()
    {
        _currentCharacterIndex = 0;
        _startCharacter = _charactersOrder[_currentCharacterIndex];
        _playerControl.SetActiveCharacter(_charactersOrder[_currentCharacterIndex]);
    }

    private void Update()
    {
        if(_turn == Turn.Player)
        {
            _playerControl.GetInput();
            return;
        }

        _currentNpc?.Act();
    }

    public void PlayerEndTurn()
    {
        SwitchTurn();
    }

    private void SwitchTurn()
    {
        /*_currentCharacterIndex++;
        if(_currentCharacterIndex >= _charactersOrder.Count)
        {
            _currentCharacterIndex = 0;
        }*/

        _turn = _charactersOrder[_currentCharacterIndex++ % _charactersOrder.Count]
            .GetComponent<ICharacter>()
            .Side;
        if(_turn == Turn.Player)
        {
            _playerControl.SetActiveCharacter(_charactersOrder[_currentCharacterIndex]);
            return;
        }

        _currentNpc = _charactersOrder[_currentCharacterIndex].GetComponent<EntityBehaviour>();
    }


}
