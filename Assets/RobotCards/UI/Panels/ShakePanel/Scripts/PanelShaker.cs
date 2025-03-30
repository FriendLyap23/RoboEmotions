using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelShaker : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private Image _leftPanel;
    [SerializeField] private Image _rightPanel;

    [Header("Shake Settings")]
    [SerializeField] private float _panelShakeStrength = 5f;
    [SerializeField] private float _panelShakeDuration = 0.5f;
    [SerializeField] private float _activationThreshold = 0.3f;

    private bool _isLeftPanelShaking = false;
    private bool _isRightPanelShaking = false;

    public void UpdateShaking(float dragDirectionNormalized)
    {
        float dragAmount = Mathf.Abs(dragDirectionNormalized);

        if (dragAmount > _activationThreshold)
        {
            if (dragDirectionNormalized > 0)
            {
                ShakePanel(_rightPanel, ref _isRightPanelShaking);
                StopShakingPanel(_leftPanel, ref _isLeftPanelShaking);
            }
            else
            {
                ShakePanel(_leftPanel, ref _isLeftPanelShaking);
                StopShakingPanel(_rightPanel, ref _isRightPanelShaking);
            }
        }
        else
        {
            StopShakingPanel(_leftPanel, ref _isLeftPanelShaking);
            StopShakingPanel(_rightPanel, ref _isRightPanelShaking);
        }
    }

    public void StopAllShaking()
    {
        StopShakingPanel(_leftPanel, ref _isLeftPanelShaking);
        StopShakingPanel(_rightPanel, ref _isRightPanelShaking);
    }

    private void ShakePanel(Image panel, ref bool isShaking)
    {
        if (panel == null || isShaking) return;

        isShaking = true;
        panel.transform.DOShakeRotation(_panelShakeDuration, _panelShakeStrength, 10, 90, true)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    private void StopShakingPanel(Image panel, ref bool isShaking)
    {
        if (panel == null || !isShaking) return;

        isShaking = false;
        panel.transform.DOKill();
        panel.transform.rotation = Quaternion.identity;
    }

    private void OnDisable()
    {
        StopAllShaking();
    }
}