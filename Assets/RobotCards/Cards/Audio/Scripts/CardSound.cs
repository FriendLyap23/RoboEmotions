using UnityEngine;

public class CardSound : MonoBehaviour
{
    [SerializeField] private AudioClip _startDragSound;
    [SerializeField] private AudioClip _returnSound;
    [SerializeField] private AudioSource _audioSource;

    private bool _soundPlayedOnDrag = false;

    public void OnBeginDrag()
    {
        _soundPlayedOnDrag = false;
        PlaySound(_startDragSound);
    }

    public void OnDrag(float currentX, float initialX)
    {
        if (!_soundPlayedOnDrag && Mathf.Abs(currentX - initialX) > 20f)
        {
            _soundPlayedOnDrag = true;
            PlaySound(_startDragSound);
        }
    }

    public void OnReturn()
    {
        PlaySound(_returnSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (_audioSource != null && clip != null)
            _audioSource.PlayOneShot(clip); 
    }
}
