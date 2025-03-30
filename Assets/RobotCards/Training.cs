using UnityEngine;
using DG.Tweening;

public class CardTraining : MonoBehaviour
{
    [SerializeField] private float _maxDragDistance = 200f;
    [SerializeField] private float _maxTiltAngle = 15f;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private float _pauseDuration = 0.5f;

    private RectTransform _rectTransform;
    private Vector2 _initialPosition;
    private Sequence _trainingSequence;
    private bool _isTrainingActive = true;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _initialPosition = _rectTransform.anchoredPosition;
    }

    private void Start()
    {
        StartTrainingAnimation();
    }

    public void StartTrainingAnimation()
    {
        if (!_isTrainingActive) return;

        _trainingSequence = DOTween.Sequence();

        _trainingSequence
            .Append(_rectTransform.DOAnchorPosX(_initialPosition.x - _maxDragDistance, _moveDuration))
            .Join(_rectTransform.DORotate(new Vector3(0, 0, _maxTiltAngle), _moveDuration))
            .AppendInterval(_pauseDuration);

        _trainingSequence
            .Append(_rectTransform.DOAnchorPosX(_initialPosition.x, _moveDuration))
            .Join(_rectTransform.DORotate(Vector3.zero, _moveDuration))
            .AppendInterval(_pauseDuration);

        _trainingSequence
            .Append(_rectTransform.DOAnchorPosX(_initialPosition.x + _maxDragDistance, _moveDuration))
            .Join(_rectTransform.DORotate(new Vector3(0, 0, -_maxTiltAngle), _moveDuration))
            .AppendInterval(_pauseDuration);

        _trainingSequence
            .Append(_rectTransform.DOAnchorPosX(_initialPosition.x, _moveDuration))
            .Join(_rectTransform.DORotate(Vector3.zero, _moveDuration))
            .AppendInterval(_pauseDuration);

        _trainingSequence.SetLoops(-1, LoopType.Restart);
    }

    public void StopTrainingAnimation()
    {
        _isTrainingActive = false;
        _trainingSequence?.Kill();
        ResetCardPosition();
    }

    private void ResetCardPosition()
    {
        _rectTransform.anchoredPosition = _initialPosition;
        _rectTransform.localEulerAngles = Vector3.zero;
    }

    public void OnFirstInteraction()
    {
        StopTrainingAnimation();
    }
}