using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SlimeScience.Ad
{
    public class AdvertismentCanvas : MonoBehaviour
    {
        private const string AdShowTimePhraseKey = "ad_show_time";
        private const int TimerTick = 1;
        private const int TimerTicksCount = 2;
        private const int ResetTime = 70;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private Canvas _canvas;

        private bool _available = true;
        private Coroutine _showPopup;
        private Coroutine _reset;

        public event Action Ended;

        public bool Available => _available;

        private void OnEnable()
        {
            _canvas.enabled = false;
        }

        private void OnDisable()
        {
            if (_showPopup != null)
            {
                StopCoroutine(_showPopup);
            }

            if (_reset != null)
            {
                StopCoroutine(_reset);
            }
        }

        public void ShowPopup()
        {
            _available = false;
            _canvas.enabled = true;

            if (_showPopup != null)
            {
                StopCoroutine(_showPopup);
            }

            _showPopup = StartCoroutine(StartTimer());
            _reset = StartCoroutine(ResetAvailable());
        }

        private IEnumerator StartTimer()
        {
            var delay = new WaitForSecondsRealtime(TimerTick);
            int ticks = 0;

            while (ticks < TimerTicksCount)
            {
                Render(TimerTicksCount - ticks);
                yield return delay;
                ticks++;
            }

            Ended?.Invoke();
            _canvas.enabled = false;
        }

        private IEnumerator ResetAvailable()
        {
            yield return new WaitForSeconds(ResetTime);
            _available = true;
        }

        private void Render(int secondsLeft)
        {
            string text = Lean.Localization.LeanLocalization.GetTranslationText(AdShowTimePhraseKey);
            _text.text = string.Format(text, secondsLeft);
        }
    }
}
