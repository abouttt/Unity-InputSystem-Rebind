using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSystem : MonoBehaviour
{
    public InputActionAsset Actions;

    private readonly string SaveKey = "Rebinds";
    private InputActionRebindingExtensions.RebindingOperation _rebindingOp;
    private string _currentPath = null;

    private void Awake()
    {
        Load();
    }

    public void Rebind(InputAction action, Action completed, Action canceld)
    {
        action.Disable();
        _currentPath = action.bindings[0].hasOverrides ? action.bindings[0].overridePath : action.bindings[0].path;
        _rebindingOp = action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/rightButton")
            .WithCancelingThrough("<Mouse>/leftButton")
            .OnComplete(op => RebindComplete(action, completed, canceld))
            .OnCancel(op => RebindCancel(action, canceld))
            .Start();
    }

    public void RestoreDefaults()
    {
        foreach (var action in Actions)
        {
            action.RemoveAllBindingOverrides();
        }
    }

    public void Save()
    {
        var rebinds = Actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(SaveKey, rebinds);
    }

    public void Load()
    {
        var rebinds = PlayerPrefs.GetString(SaveKey);
        Actions.LoadBindingOverridesFromJson(rebinds);
    }

    private void OnDestroy()
    {
        Save();
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
        _rebindingOp.Dispose();
        _currentPath = null;
    }
}
