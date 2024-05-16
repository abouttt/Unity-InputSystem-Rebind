using UnityEngine;
using UnityEngine.UI;

public class UI_RebindSystem : MonoBehaviour
{
    public GameObject PressUI;

    private RebindSystem _rebindSystem;
    private UI_Key[] _keys;

    private void Awake()
    {
        _rebindSystem = FindObjectOfType<RebindSystem>();
        _keys = GetComponentsInChildren<UI_Key>();
        foreach (var key in _keys)
        {
            key.GetComponent<Button>().onClick.AddListener(() =>
            {
                PressUI.SetActive(true);
                key.KeyText.text = key.SelectedKeyText;
                _rebindSystem.Rebind(key.ActionRef.action, () => EndedRebind(key), () => EndedRebind(key));
            });
        }
    }

    private void Start()
    {
        PressUI.SetActive(false);
    }

    public void OnReset()
    {
        _rebindSystem.RestoreDefaults();

        foreach (var key in _keys)
        {
            key.RefreshBindingDisplayText();
        }
    }

    private void EndedRebind(UI_Key key)
    {
        PressUI.SetActive(false);
        key.RefreshBindingDisplayText();
    }
}
