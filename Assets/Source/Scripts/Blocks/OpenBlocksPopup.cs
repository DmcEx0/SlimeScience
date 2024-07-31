using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Blocks
{
    public class OpenBlocksPopup : MonoBehaviour
    {
        private const float OpenDuration = 0.35f;
        private const float CloseDuration = 0.35f;
        
        [SerializeField] private Button _close;
        
        private Tweener _openTweener;
        private Tweener _closeTweener;

        private void OnEnable()
        {
            _close.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _close.onClick.RemoveListener(Close);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
            _openTweener?.Kill();
            _openTweener = transform
                .DOScale(Vector3.one, OpenDuration)
                .SetEase(Ease.OutBack);
        }

        private void Close()
        {
            _closeTweener?.Kill();

            _closeTweener = transform
                .DOScale(Vector3.zero, CloseDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }
    }
}
