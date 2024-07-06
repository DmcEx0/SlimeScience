using System;
using System.Collections;
using Agava.YandexGames;
using DG.Tweening;
using Lean.Localization;
using SlimeScience.Pool;
using SlimeScience.Util;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Leaderbords
{
    public class LeaderbordCanvas : MonoBehaviour
    {
        private const string LeaderbordName = "TotalSlimes";

        private const float OpenDuration = 0.35f;
        private const float CloseDuration = 0.35f;
        private const int PlayersCount = 10;

        [SerializeField] private Button[] _closeButtons;

        [SerializeField] private Canvas _leaderbord;
        [SerializeField] private RectTransform _authView;
        [SerializeField] private RectTransform _nonAuthView;

        [SerializeField] private RectTransform _content;
        [SerializeField] private LBPlayerContainer _playerContainerPrefab;

        private Tweener _openTweener;
        private Tweener _closeTweener;

        private ObjectPool<LBPlayerContainer> _playerContainerPool;

        public event Action Closed;

        private void OnEnable()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.AddListener(OnCloseCanvasButtonClicked);
            }
        }

        private void OnDisable()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.RemoveListener(OnCloseCanvasButtonClicked);
            }
        }

        public void Init()
        {
            _playerContainerPool = new ObjectPool<LBPlayerContainer>(
                _playerContainerPrefab,
                PlayersCount,
                _content);

            _authView.transform.localScale = Vector3.zero;
            _nonAuthView.transform.localScale = Vector3.zero;

            RenderView();
        }

        public void Open()
        {
            _leaderbord.gameObject.SetActive(true);

            var activeView = GetActiveView();
            activeView.gameObject.SetActive(true);

            RenderView();

            _openTweener?.Kill();
            _openTweener = activeView.transform
                .DOScale(Vector3.one, OpenDuration)
                .SetEase(Ease.OutBack);
        }

        private void OnCloseCanvasButtonClicked()
        {
            var activeView = GetActiveView();

            _closeTweener?.Kill();

            _closeTweener = activeView.transform
                .DOScale(Vector3.zero, CloseDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                    {
                        _leaderbord.gameObject.SetActive(false);
                        activeView.gameObject.SetActive(false);
                        Closed?.Invoke();
                    });
            
            SoundsManager.PlayTapUI();
        }

        private void GetLeaderbordData()
        {
           _playerContainerPool.Reset();

           if (PlayerAccount.IsAuthorized == false)
           {
               return;
           }

           Leaderboard.GetEntries(LeaderbordName, (result) =>
           {
              Debug.Log("Leaderboard.GetEntries: " + result);
              int length = result.entries.Length > PlayersCount ? PlayersCount : result.entries.Length;

              if (length == 0)
              {
                  return;
              }

              for (int i = 0; i < length; i++)
              {
                  var entry = result.entries[i];
                  if (entry.score == 0)
                  {
                      continue;
                  }

                  var container = _playerContainerPool.GetAvailable();
                  container.gameObject.SetActive(true);
                  container.Render(entry);
              }
           });
        }

        private void RenderNonAuthView()
        {
            _nonAuthView.gameObject.SetActive(true);
            _authView.gameObject.SetActive(false);
        }

        private void RenderAuthView()
        {
            _nonAuthView.gameObject.SetActive(false);
            _authView.gameObject.SetActive(true);

            GetLeaderbordData();
        }

        private RectTransform GetActiveView()
        {
#if UNITY_EDITOR
            return _nonAuthView;
#else
            return PlayerAccount.IsAuthorized ? _authView : _nonAuthView;
#endif
        }

        private void RenderView()
        {
#if UNITY_EDITOR
            RenderNonAuthView();
#elif UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
            {
                RenderAuthView();
            }
            else
            {
                RenderNonAuthView();
            }
#endif
        }
    }
}
