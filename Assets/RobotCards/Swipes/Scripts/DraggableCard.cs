using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _maxDragDistance;
    [SerializeField] private float _maxTiltAngle;

    [SerializeField] private Canvas _canvas;

    [SerializeField] private ShowCards showCards;
    [SerializeField] private PanelShaker _panelShaker;
    [SerializeField] private CardSound _cardSound;

    [SerializeField] private CardTraining _cardTraining;

    private RectTransform rectTransform;
    private bool isDragging = false;
    private float _canvasScale;

    public Vector2 initialPosition;

    public float MaxDragDistance => _maxDragDistance;
    public float CanvasScale => _canvasScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        _canvasScale = _canvas.transform.localScale.x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        _cardTraining?.OnFirstInteraction();
        _cardSound.OnBeginDrag();

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            float newX = rectTransform.anchoredPosition.x + eventData.delta.x;
            newX = Mathf.Clamp(newX, initialPosition.x - _maxDragDistance, initialPosition.x + _maxDragDistance);

            rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);

            float tilt = (newX - initialPosition.x) / _maxDragDistance * _maxTiltAngle;
            rectTransform.localEulerAngles = new Vector3(0, 0, -tilt);

            _cardSound.OnDrag(newX, initialPosition.x);
            _panelShaker.UpdateShaking((newX - initialPosition.x) / _maxDragDistance);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        _panelShaker.StopAllShaking();

        float deltaX = (rectTransform.anchoredPosition.x - initialPosition.x) * _canvasScale;
        float percentageDragged = Mathf.Abs(deltaX) / (_maxDragDistance * _canvasScale);

        if (percentageDragged > 0.5f)
        {
            HandleSwipe(deltaX > 0);
        }
        else
        {
            _cardSound.OnReturn();
            ResetCardPosition();
        }
    }

    private void HandleSwipe(bool isRightSwipe)
    {
        showCards.OnChoiceSelected(isRightSwipe);

        float targetX = isRightSwipe ? Screen.width : -Screen.width;
        rectTransform.DOAnchorPosX(targetX, 0.5f).OnComplete(() =>
        {
            ResetCardPosition();
            showCards.StartNewRound();
        });
    }

    private void ResetCardPosition()
    {
        rectTransform.DOAnchorPos(initialPosition, 0.3f).SetEase(Ease.OutBounce);
        rectTransform.DORotate(Vector3.zero, 0.3f);
    }
}
