using Assets._Project.Scripts.Data;
using Assets._Project.Scripts.Entity;
using Assets._Project.Scripts.VersionControl;
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
    public int CurrentCharacterIndex => _currentCharacterIndex;
    public Turn CurrentTurn => _turn;

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
        if (_turn == Turn.Player)
        {
            _playerControl.GetInput();
            return;
        }

        _currentNpc?.Act();
    }

    public void EndTurn()
    {
        _currentCharacterIndex++;
        if(_currentCharacterIndex >= _charactersOrder.Count)
        {
            _currentCharacterIndex = 0;
        }

        var character = _charactersOrder[_currentCharacterIndex].GetComponent<ICharacter>();
        character.ResetTurn();
        _turn = character.Side;
        if (_turn == Turn.Player)
        {
            _playerControl.SetActiveCharacter(_charactersOrder[_currentCharacterIndex]);
            return;
        }

        _currentNpc = _charactersOrder[_currentCharacterIndex].GetComponent<EntityBehaviour>();
    }

    public void LoadNewState(Commit commit)
    {
        _turn = commit.CurrentTurn;
        _currentCharacterIndex = commit.CurrentCharacterIndexTurn;

        for(int i = 0; i < _charactersOrder.Count; i++)
        {
            _charactersOrder[i].GetComponent<Character>().SetNewData(commit.CharactersData[i]);
        }
    }
}
