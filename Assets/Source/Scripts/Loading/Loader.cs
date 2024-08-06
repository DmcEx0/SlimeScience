using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Loading
{
    public class Loader : MonoBehaviour
    {
        private const float ScaleTime = 1.5f;

        [SerializeField] private Image _spiner;

        private Tweener _tweener;
        private Vector3 _rotation = new Vector3(0, 0, -360);
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.one;

            _tweener = _spiner.transform.DORotate(_rotation, 1f, RotateMode.FastBeyond360).SetLoops(-1);
        }

        private void OnDisable()
        {
            if (_tweener != null)
            {
                _tweener.Kill();
            }

            Disable();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (gameObject.activeSelf)
            {
                transform
                    .DOScale(Vector3.zero, ScaleTime)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => gameObject.SetActive(false));
            }
        }
    }
}
