using Assets._Project.Scripts.VersionControl;
using Assets._Project.Scripts.Data;
using UnityEngine;
using System.Collections.Generic;
using Assets._Project.Scripts.Entity;
using UnityEngine.UI;
using Assets._Project.Scripts;
using System.Linq;
using TMPro;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private EntityCommandInvokerInstance _commandInvoker;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private Transform _commitsViewArea;
    [SerializeField] private Transform _branchesViewArea;
    [SerializeField] private GameObject _buttonPrefab;

    private VersionController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = new VersionController();

        UpdateBranchesList();
    }

    public void CreateNewCommit()
    {
        Commit newCommit = new Commit(
            _turnController.CurrentCharacterIndex, 
            GenerateWorldData(), 
            _turnController.CurrentTurn, 
            _commandInvoker.CommandInvoker.ExecutedCommands);

        _controller.AddNewCommit(newCommit);

        UpdateBranchesList();
        UpdateNextCommitsList();
    }

    public void LoadLastCommit()
    {
        var commit = _controller.LastCommit;
        LoadNewState(commit);

        UpdateNextCommitsList();
    }

    public void LoadPreviousCommit()
    {
        var commit = _controller.GetPreviousCommit();
        LoadNewState(commit);

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

            button.GetComponentInChildren<TMP_Text>().text = _controller.CurrentCommit.Next[i].Guid.ToString();
        }
    }

    private void UpdateBranchesList()
    {
        foreach (Transform button in _branchesViewArea)
        {
            Destroy(button.gameObject);
        }

        for (int i = 0; i < _controller.Branches.Count; i++)
        {
            GameObject button = Instantiate(_buttonPrefab);
            button.transform.SetParent(_branchesViewArea.transform, false);

            var index = i;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                _controller.SwitchBranch(_controller.Branches[index].Guid);
                UpdateBranchesList();
            });

            button.GetComponentInChildren<TMP_Text>().text = _controller.Branches[index].Guid.ToString();
            if(button.GetComponentInChildren<TMP_Text>().text == _controller.CurrentBranch.Guid.ToString())
            {
                button.GetComponent<Image>().color = Color.green;
            }
        }
    }

    private void LoadNewState(Commit commitToLoad)
    {
        _turnController.LoadNewState(commitToLoad);
        _commandInvoker.CommandInvoker.ExecutedCommands = commitToLoad.ExecutedCommands;
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
