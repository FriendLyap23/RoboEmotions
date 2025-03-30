using UnityEngine;

public class SubtitlesAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        if (_animator != null) 
            _animator.SetTrigger("Start");
    }
}
