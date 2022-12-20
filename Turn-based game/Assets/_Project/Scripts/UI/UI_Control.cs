using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private GameObject _versionControlUI;

    private bool _isMenuActive;

    private void Start()
    {
        _isMenuActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _isMenuActive = !_isMenuActive;
            _versionControlUI.SetActive(_isMenuActive);
        }
    }
}
