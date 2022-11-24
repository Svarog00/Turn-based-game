using Assets._Project.Scripts.VersionControl;
using Assets._Project.Scripts.Data;
using UnityEngine;
using System.Collections.Generic;
using Assets._Project.Scripts.Entity;
using System;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private TurnController _turnController;
    [SerializeField] private Transform _commitsViewArea;
    [SerializeField] private GameObject _buttonPrefab;

    private VersionController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = new VersionController();
    }

    public void CreateNewCommit()
    {
        Commit newCommit = new Commit(_turnController.CurrentCharacterIndex, GenerateWorldData(), _turnController.CurrentTurn);
        _controller.AddNewCommit(newCommit);
    }

    public void LoadLastCommit()
    {
        var commit = _controller.CurrentCommit;
        _turnController.LoadNewState(commit);
    }

    public void LoadPreviousCommit()
    {
        Commit commit = _controller.GetPreviousCommit();
        _turnController.LoadNewState(commit);

        UpdateNextCommitsList();
    }

    private void UpdateNextCommitsList()
    {
        foreach(Transform button in _commitsViewArea)
        {
            Destroy(button.gameObject);
        }

        for(int i = 0; i < _controller.CurrentCommit.Next.Count; i++)
        {
            GameObject button = Instantiate(_buttonPrefab);
            button.transform.SetParent(_commitsViewArea.transform, false);

            var index = i;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                var commit = _controller.GetNextCommit(_controller.CurrentCommit.Next[index].Guid);
                _turnController.LoadNewState(commit);
                UpdateNextCommitsList();
            });

            button.GetComponentInChildren<Text>().text = _controller.CurrentCommit.Next[i].Guid.ToString();
        }
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
