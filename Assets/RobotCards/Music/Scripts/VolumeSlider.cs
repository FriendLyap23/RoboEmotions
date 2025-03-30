using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;

    private const string VolumeKey = "GameVolume";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);

        _volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
    }
}
