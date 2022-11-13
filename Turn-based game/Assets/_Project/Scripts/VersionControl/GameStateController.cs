using Assets._Project.Scripts.VersionControl;
using Assets._Project.Scripts.Data;
using UnityEngine;
using System.Collections.Generic;
using Assets._Project.Scripts.Entity;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private TurnController _turnController;

    private VersionController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = new VersionController();
    }

    public void CreateNewCommit()
    {
        Commit newCommit = new Commit(_turnController.CurrentCharacterIndex, GenerateWorldData());
        _controller.AddNewCommit(newCommit);
    }

    public void LoadCurrentCommit()
    {
        
    }

    private List<CharacterWorldData> GenerateWorldData()
    {
        List<CharacterWorldData> worldData = new List<CharacterWorldData>();
        foreach (var character in _turnController.Characters)
        {
            worldData.Add(character.GetComponent<Character>().GenerateWorldData());
        }

        return worldData;
    }
}
