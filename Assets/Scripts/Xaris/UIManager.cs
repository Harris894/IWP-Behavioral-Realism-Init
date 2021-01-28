using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel;

    [SerializeField]
    private InputActionAsset actionAsset;
    [SerializeField] private string controllerName;
    [SerializeField] private string actionNameButtonY;

    private InputActionMap _actionMap;
    private InputAction _inputActionButtonY;


    private void Awake()
    {
        _actionMap = actionAsset.FindActionMap(controllerName);
        _inputActionButtonY = _actionMap.FindAction(actionNameButtonY);
        
    }

    void OnEnable()
    {
        _inputActionButtonY.Enable();
    }
    private void OnDisable()
    {
        _inputActionButtonY.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        if (_inputActionButtonY.triggered)
        {
            Debug.Log("Y button Pressed");
            if (!uiPanel.activeSelf)
            {
                uiPanel.SetActive(true);
            }
            else
            {
                uiPanel.SetActive(false);
            }
        }

    }

    
}
