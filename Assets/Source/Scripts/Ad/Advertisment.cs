using SlimeScience.PauseSystem;
using System.Collections;
using UnityEngine;

namespace SlimeScience.Ad
{
    public class Advertisment
    {
        private const float ShowInterval = 65f;

        private Coroutine _schedule;
        private PauseHandler _systemPause;
        private MonoBehaviour _coroutineObject;

        private bool _isActive = false;

        public Advertisment(MonoBehaviour coroutineObject, PauseHandler pause)
        {
            _coroutineObject = coroutineObject;
            _systemPause = pause;
        }

        public void StartIntervalShow()
        {
            _isActive = true;

            if (_schedule != null)
            {
                _coroutineObject.StopCoroutine(_schedule);
            };

            _schedule = _coroutineObject.StartCoroutine(ScheduleShow());
        }

        public void StopIntervalShow()
        {
            _isActive = false;

            if (_schedule != null)
            {
                _coroutineObject.StopCoroutine(_schedule);
            };
        }

        private IEnumerator ScheduleShow()
        {
            var delay = new WaitForSeconds(ShowInterval);

            while (_isActive) {
                Show();
                yield return delay;
            }
        }

        private void Show()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Agava.YandexGames.InterstitialAd.Show(
                onOpenCallback: OnOpenedCallback,
                onCloseCallback: OnClosedCallback,
                onErrorCallback: OnErrorCallback,
                onOfflineCallback: OnOfflineCallback);
#endif
        }

        private void OnOpenedCallback()
        {
            _systemPause.Pause();
        }

        private void OnClosedCallback(bool isClosed)
        {
            _systemPause.Unpause();
        }

        private void OnErrorCallback(string error)
        {
            Debug.LogError($"Ad error: {error}");
           _systemPause.Unpause();
        }

        private void OnOfflineCallback()
        {
            _systemPause.Unpause();
        }
    }
}
