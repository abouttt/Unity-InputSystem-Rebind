using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSystem : MonoBehaviour
{
    public GameObject PressUI;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOp;
    private string _currentPath = null;

    private void Start()
    {
        PressUI.SetActive(false);
    }

    public void Rebind(InputAction action, Action completed, Action canceld)
    {
        PressUI.SetActive(true);

        action.Disable();
        _currentPath = action.bindings[0].hasOverrides ? action.bindings[0].overridePath : action.bindings[0].path;
        _rebindingOp = action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/rightButton")
            .WithCancelingThrough("<Mouse>/leftButton")
            .OnCancel(op => RebindCancel(action, canceld))
            .OnComplete(op => RebindComplete(action, completed, canceld))
            .Start();
    }

    private void RebindCancel(InputAction action, Action callback)
    {
        Clear();
        action.Enable();
        callback?.Invoke();
    }

    private void RebindComplete(InputAction action, Action completed, Action canceld)
    {
        if (HasSameBinding(action))
        {
            if (_currentPath != null)
            {
                action.ApplyBindingOverride(_currentPath);
            }

            RebindCancel(action, canceld);
        }
        else
        {
            Clear();
            action.Enable();
            completed?.Invoke();
        }
    }

    private bool HasSameBinding(InputAction action)
    {
        var newBinding = action.bindings[0];
        foreach (var binding in action.actionMap.bindings)
        {
            if (binding.action == newBinding.action)
            {
                continue;
            }

            if (binding.effectivePath == newBinding.effectivePath)
            {
                Debug.Log("Has same binding : " + newBinding.effectivePath);
                return true;
            }
        }

        return false;
    }

    private void Clear()
    {
        PressUI.SetActive(false);
        _rebindingOp.Dispose();
        _currentPath = null;
    }
}
