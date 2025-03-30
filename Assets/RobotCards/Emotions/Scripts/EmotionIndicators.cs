using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EmotionIndicators : MonoBehaviour
{
    [Header("Indicators")]
    [SerializeField] private Image _angryIndicator;
    [SerializeField] private Image _sadIndicator;
    [SerializeField] private Image _fearIndicator;
    [SerializeField] private Image _joyIndicator;

    private void Start()
    {
        ResetAllIndicators();
    }

    public void ShowEmotionChanges(Emotions emotionChanges)
    {
        ResetAllIndicators();

        _angryIndicator.gameObject.SetActive(emotionChanges.AngerChange != 0);
        _sadIndicator.gameObject.SetActive(emotionChanges.SadnessChange != 0);
        _fearIndicator.gameObject.SetActive(emotionChanges.FearChange != 0);
        _joyIndicator.gameObject.SetActive(emotionChanges.JoyChange != 0);
    }

    public void ResetAllIndicators()
    {
        if (_angryIndicator != null) _angryIndicator.gameObject.SetActive(false);
        if (_sadIndicator != null) _sadIndicator.gameObject.SetActive(false);
        if (_fearIndicator != null) _fearIndicator.gameObject.SetActive(false);
        if (_joyIndicator != null) _joyIndicator.gameObject.SetActive(false);
    }
}
