using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPanel : Panel
{
    [Serializable]
    public class FinalText
    {
        public int endingIndex;
        [TextArea(3, 10)]
        public string text;
        public AudioClip typingSound;
    }

    [SerializeField] private FinalText[] _finalTexts;
    [SerializeField] private TMP_Text _finalTextDisplay;
    [SerializeField] private TMP_Text _hintText;
    [SerializeField] private int _sceneToLoad;
    [SerializeField] private float _hintDelay = 1f;

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

    public void ShowFinalText(int endingIndex)
    {
        var final = Array.Find(_finalTexts, f => f.endingIndex == endingIndex);
        if (final != null)
        {
            string username = System.Environment.UserName;
            string processedText = final.text.Replace("[ИмяПользователя]", username);

            OpenPanel();
            _textPrinter.PrintText(_finalTextDisplay, processedText, OnTextPrinted);
        }
    }

    private void OnTextPrinted()
    {
        StartCoroutine(ShowHintAndWaitForInput());
    }

    private IEnumerator ShowHintAndWaitForInput()
    {
        yield return new WaitForSeconds(_hintDelay);

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
