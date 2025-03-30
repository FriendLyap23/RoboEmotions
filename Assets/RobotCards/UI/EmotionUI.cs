using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EmotionUI : MonoBehaviour
{
    [SerializeField] private Image AngryFill;
    [SerializeField] private Image SadnessFill;
    [SerializeField] private Image FearFill;
    [SerializeField] private Image JoyFill;

    private Emotion _emotion;

    [Inject]
    private void Constructor(Emotion emotion) 
    {
        _emotion = emotion;
        _emotion.OnEmotionsUpdated += UpdateValueEmotion;
    }

    private void UpdateValueEmotion() 
    {
        JoyFill.fillAmount = _emotion.Joy / 100f;
        SadnessFill.fillAmount = _emotion.Sadness / 100f;
        FearFill.fillAmount = _emotion.Fear / 100f;
        AngryFill.fillAmount = _emotion.Angry / 100f;
    }

    private void OnDisable()
    {
        _emotion.OnEmotionsUpdated -= UpdateValueEmotion;
    }
}
