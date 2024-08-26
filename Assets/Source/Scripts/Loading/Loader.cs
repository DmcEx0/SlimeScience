using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Loading
{
    public class Loader : MonoBehaviour
    {
        private const float ScaleTime = 1.5f;

        [SerializeField] private Image _spiner;

        private Tweener _spinerTweener;
        private Tweener _disableTweener;
        private Vector3 _rotation = new Vector3(0, 0, -360);
        private Vector3 _originalScale;

        public event Action Hidden;

        private void Awake()
        {
            _originalScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.one;

            _spinerTweener = _spiner.transform.DORotate(_rotation, 1f, RotateMode.FastBeyond360).SetLoops(-1);
        }

        private void OnDisable()
        {
            if (_spinerTweener != null)
            {
                _spinerTweener.Kill();
            }

            Disable();
        }

        private void OnDestroy()
        {
            if (_disableTweener != null)
            {
                _disableTweener.Kill();
            }

            if (_spinerTweener != null)
            {
                _spinerTweener.Kill();
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (_disableTweener != null)
            {
                _disableTweener.Kill();
            }

            if (gameObject.activeSelf)
            {
                _disableTweener = transform
                    .DOScale(Vector3.zero, ScaleTime)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                        Hidden?.Invoke();
                    });
            }
        }
    }
}
