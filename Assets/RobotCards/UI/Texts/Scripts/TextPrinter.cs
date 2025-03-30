using System.Collections;
using TMPro;
using UnityEngine;

public class TextPrinter : MonoBehaviour
{
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] private AudioClip _typingSound;
    [SerializeField] private float _soundPitchRandomization = 0.1f;

    private AudioSource _audioSource;
    private Coroutine _typingCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    public void PrintText(TMP_Text textField, string text, System.Action onComplete = null)
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }
        _typingCoroutine = StartCoroutine(TypeTextCoroutine(textField, text, onComplete));
    }

    public void StopPrinting()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
        _audioSource.Stop();
    }

    private IEnumerator TypeTextCoroutine(TMP_Text textField, string text, System.Action onComplete)
    {
        textField.text = "";
        var waitTime = new WaitForSeconds(_typingSpeed);

        foreach (char letter in text.ToCharArray())
        {
            // Печать буквы
            textField.text += letter;

            // Воспроизведение звука для каждой буквы (кроме пробелов)
            if (letter != ' ' && _typingSound != null)
            {
                PlayLetterSound();
            }

            // Ожидание перед следующей буквой
            yield return waitTime;
        }

        _typingCoroutine = null;
        onComplete?.Invoke();
    }

    private void PlayLetterSound()
    {
        if (_audioSource == null || _typingSound == null) return;

        // Случайное изменение тона для разнообразия
        _audioSource.pitch = 1f + Random.Range(-_soundPitchRandomization, _soundPitchRandomization);

        // Останавливаем предыдущий звук, если он еще играет
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.PlayOneShot(_typingSound);
    }
}

