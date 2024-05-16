using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.actions.Enable();
    }

    private void OnDisable()
    {
        _playerInput.actions.Disable();
    }

    private void OnSkill01(InputValue inputValue)
    {
        Debug.Log("Skill 01");
    }

    private void OnSkill02(InputValue inputValue)
    {
        Debug.Log("Skill 02");
    }

    private void OnSkill03(InputValue inputValue)
    {
        Debug.Log("Skill 03");
    }

    private void OnSkill04(InputValue inputValue)
    {
        Debug.Log("Skill 04");
    }

    private void OnSpell01(InputValue inputValue)
    {
        Debug.Log("Spell 01");
    }

    private void OnSpell02(InputValue inputValue)
    {
        Debug.Log("Spell 02");
    }

    private void OnItem01(InputValue inputValue)
    {
        Debug.Log("Item 01");
    }

    private void OnItem02(InputValue inputValue)
    {
        Debug.Log("Item 02");
    }

    private void OnItem03(InputValue inputValue)
    {
        Debug.Log("Item 03");
    }

    private void OnItem04(InputValue inputValue)
    {
        Debug.Log("Item 04");
    }

    private void OnItem05(InputValue inputValue)
    {
        Debug.Log("Item 05");
    }

    private void OnItem06(InputValue inputValue)
    {
        Debug.Log("Item 06");
    }

    private void OnAccessories(InputValue inputValue)
    {
        Debug.Log("Accessories");
    }
}
