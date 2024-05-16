using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UI_Key : MonoBehaviour
{
    public TextMeshProUGUI KeyText;
    public InputActionReference ActionRef;
    public string SelectedKeyText;

    private RebindSystem _rebindSystem;

    private void Awake()
    {
        _rebindSystem = FindObjectOfType<RebindSystem>();
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void Start()
    {
        RefreshBindingDisplayText();
    }

    private void OnButtonClick()
    {
        KeyText.text = SelectedKeyText;
        _rebindSystem.Rebind(ActionRef.action, RefreshBindingDisplayText, RefreshBindingDisplayText);
    }

    private void RefreshBindingDisplayText()
    {
        KeyText.text = ActionRef.action.GetBindingDisplayString();
    }
}
