using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : Panel
{
    [SerializeField] private TMP_Text _textDisplay;
    [SerializeField] private TMP_Text _hintText;

    [SerializeField] private string _loseText;

    [SerializeField] private int _sceneToLoad = 1;

    private TextPrinter _textPrinter;
    private bool _isWaitingForInput;

    private void Awake()
    {
        _textPrinter = GetComponent<TextPrinter>();
        if (_textPrinter == null)
        {
            _textPrinter = gameObject.AddComponent<TextPrinter>();
        }
    }

    public void ShowLoseScreen()
    {
        OpenPanel();
        _textPrinter.PrintText(_textDisplay, _loseText, OnTextPrinted);
    }

    private void OnTextPrinted()
    {
        StartCoroutine(ShowHintAndWaitForInput());
    }

    private IEnumerator ShowHintAndWaitForInput()
    {
        yield return new WaitForSeconds(0.5f);
        _hintText.gameObject.SetActive(true);
        _isWaitingForInput = true;

        while (_isWaitingForInput && !Input.anyKeyDown)
        {
            yield return null;
        }

        SceneManager.LoadScene(_sceneToLoad);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        _textPrinter.StopPrinting();
        _isWaitingForInput = false;
        _hintText.gameObject.SetActive(false);
    }
}
