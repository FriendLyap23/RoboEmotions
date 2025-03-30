using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Emotion : MonoBehaviour
{
    [Header("Emotion Settings")]
    [SerializeField] private int _initialValue = 50;
    [SerializeField] private int _criticalThreshold = 100;

    [Header("Dependencies")]
    [SerializeField] private LoseScreen _loseScreenController;

    public int Fear { get; private set; }
    public int Joy { get; private set; }
    public int Sadness { get; private set; }
    public int Angry { get; private set; }

    public event Action OnEmotionsUpdated;

    private void Start() => InitializeEmotions();

    public void ApplyEffect(Emotions effect)
    {
        UpdateEmotions(effect);
        if (IsEmotionCritical())
            _loseScreenController.ShowLoseScreen();
    }

    private void InitializeEmotions()
    {
        Fear = Joy = Sadness = Angry = _initialValue;
        OnEmotionsUpdated?.Invoke();
    }

    private void UpdateEmotions(Emotions effect)
    {
        Fear = Mathf.Clamp(Fear + effect.FearChange, 0, _criticalThreshold);
        Joy = Mathf.Clamp(Joy + effect.JoyChange, 0, _criticalThreshold);
        Sadness = Mathf.Clamp(Sadness + effect.SadnessChange, 0, _criticalThreshold);
        Angry = Mathf.Clamp(Angry + effect.AngerChange, 0, _criticalThreshold);

        OnEmotionsUpdated?.Invoke();
    }

    private bool IsEmotionCritical()
    {
        return Fear <= 0 || Fear >= _criticalThreshold ||
               Joy <= 0 || Joy >= _criticalThreshold ||
               Sadness <= 0 || Sadness >= _criticalThreshold ||
               Angry <= 0 || Angry >= _criticalThreshold;
    }
}
