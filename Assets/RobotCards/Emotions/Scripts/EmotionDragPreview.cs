using UnityEngine;
using UnityEngine.EventSystems;

public class EmotionDragPreview : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private EmotionIndicators _emotionIndicators;
    [SerializeField] private ShowCards _showCards;
    [SerializeField] private DraggableCard _draggableCard;
    [SerializeField] private float _previewThreshold = 0.3f;

    private Emotions _currentEmotionPreview;

    private bool _isRightSwipePreview;
    private bool _isDragging;
    private bool _previewShown;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _previewShown = false;
        UpdateEmotionPreview();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        float currentX = _draggableCard.GetComponent<RectTransform>().anchoredPosition.x;
        float initialX = _draggableCard.initialPosition.x;
        float deltaX = currentX - initialX;

        bool newSwipeDirection = deltaX > 0;
        if (newSwipeDirection != _isRightSwipePreview)
        {
            _isRightSwipePreview = newSwipeDirection;
            UpdateEmotionPreview();
        }

        float percentageDragged = Mathf.Abs(deltaX) / (_draggableCard.MaxDragDistance * _draggableCard.CanvasScale);
        bool shouldShowPreview = percentageDragged > _previewThreshold;

        if (shouldShowPreview && !_previewShown)
        {
            _emotionIndicators.gameObject.SetActive(true);
            _emotionIndicators.ShowEmotionChanges(_currentEmotionPreview);
            _previewShown = true;
        }
        else if (!shouldShowPreview && _previewShown)
        {
            _emotionIndicators.gameObject.SetActive(false);
            _previewShown = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _previewShown = false;
        _emotionIndicators.ResetAllIndicators();
        _emotionIndicators.gameObject.SetActive(false);
    }

    private void UpdateEmotionPreview()
    {
        var card = _showCards.GetCurrentCard();
        if (card?.Choices == null || card.Choices.Length < 2) return;

        var choice = card.Choices[_isRightSwipePreview ? 1 : 0];
        _currentEmotionPreview = choice.emotions;

        if (_previewShown)
            _emotionIndicators.ShowEmotionChanges(_currentEmotionPreview);
    }
}