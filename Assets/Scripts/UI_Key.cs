using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UI_Key : MonoBehaviour
{
    public TextMeshProUGUI KeyText;
    public InputActionReference ActionRef;
    public string SelectedKeyText;

    private void Start()
    {
        RefreshBindingDisplayText();
    }

    public void RefreshBindingDisplayText()
    {
        KeyText.text = ActionRef.action.GetBindingDisplayString();
    }
}
