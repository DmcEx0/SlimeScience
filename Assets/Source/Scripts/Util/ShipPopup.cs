using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience
{
    public class ShipPopup : MonoBehaviour
    {
        private const float OpenDuration = 0.35f;
        private const float CloseDuration = 0.35f;
        
        [SerializeField] private Button _exit;
        [SerializeField] private Button _showAd;
        
        private Tweener _openTweener;
        private Tweener _closeTweener;

        public event Action AdShowing; 

        private void OnEnable()
        {
            _exit.onClick.AddListener(Close);
            _showAd.onClick.AddListener(ShowAd);
        }

        private void OnDisable()
        {
            _exit.onClick.RemoveListener(Close);
            _showAd.onClick.RemoveListener(ShowAd);
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

        private void ShowAd()
        {
            AdShowing?.Invoke();
            Close();
        }
    }
}
