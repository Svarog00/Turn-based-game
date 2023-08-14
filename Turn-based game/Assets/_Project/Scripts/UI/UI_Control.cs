using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private GameObject _versionControlUI;

    [SerializeField] private bool _isMenuActive;

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
