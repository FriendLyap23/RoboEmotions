using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] protected GameObject _panel;

    public bool IsPanelOpen => _panel != null && _panel.activeSelf;

    public virtual void OpenPanel()
    {
        _panel.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        _panel.SetActive(false);
    }
}
