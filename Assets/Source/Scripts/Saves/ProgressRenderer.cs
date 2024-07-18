using System.Collections;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Saves
{
    public class ProgressRenderer : MonoBehaviour
    {
        private const string PercentText = "{0}%";
        private const float SmoothTime = 1f;
        private const string RoomPharseKey = "room_number";
        private const string LastRoomPharseKey = "last_room_number";

        [SerializeField] private TMP_Text _roomNumber;
        [SerializeField] private TMP_Text _percentText;
        [SerializeField] private Slider _slider;

        private Coroutine _smoothSlider;
        private GameVariables _gameVariables;

        private void OnEnable()
        {
            if (_gameVariables != null)
            {
                _gameVariables.IncreasedRoomIndex += OnRoomIndexIncreased;
                _gameVariables.SlimeCollected += OnSlimeCollected;
                _gameVariables.SlimeCollectedReset += OnSlimeCollectedReset;
                _gameVariables.SlimeGoalChanged += OnSlimeGoalChanged;
            }
        }

        private void OnDisable()
        {
            if (_gameVariables != null)
            {
                _gameVariables.IncreasedRoomIndex -= OnRoomIndexIncreased;
                _gameVariables.SlimeCollected -= OnSlimeCollected;
                _gameVariables.SlimeCollectedReset -= OnSlimeCollectedReset;
                _gameVariables.SlimeGoalChanged -= OnSlimeGoalChanged;
            }
        }

        public void Init(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;
            _gameVariables.IncreasedRoomIndex += OnRoomIndexIncreased;
            _gameVariables.SlimeCollected += OnSlimeCollected;
            _gameVariables.SlimeCollectedReset += OnSlimeCollectedReset;
            _gameVariables.SlimeGoalChanged += OnSlimeGoalChanged;

            SetRoomNumber(_gameVariables.RoomIndex);
            SetSliderMaxValue(_gameVariables.SlimesGoal);
            SetSliderValue(_gameVariables.CollectedSlimes);
            SetPercent(_gameVariables.CollectedSlimes / _gameVariables.SlimesGoal * 100);
        }

        private void SetSliderMaxValue(float value)
        {
            _slider.maxValue = value;
        }

        private void SetRoomNumber(int number)
        {
            string text = LeanLocalization.GetTranslationText(RoomPharseKey);

            if (number == -1)
            {

                _roomNumber.text = string.Format(text, LeanLocalization.GetTranslationText(LastRoomPharseKey));
                return;
            }

            _roomNumber.text = string.Format(text, number + 1);
        }

        private void OnSlimeCollected(int count)
        {
            SetSmoothSliderValue(count);
        }

        private void OnSlimeCollectedReset()
        {
            SetSmoothSliderValue(0);
        }

        private void OnRoomIndexIncreased(int index)
        {
            SetRoomNumber(index);
        }

        private void OnSlimeGoalChanged(int value)
        {
            int minValue = 0;

            SetPercent(minValue);
            _slider.value = minValue;
            SetSliderMaxValue(value);
        }

        private void SetSmoothSliderValue(float value)
        {
            if (_smoothSlider != null)
            {
                StopCoroutine(_smoothSlider);
            }

            _smoothSlider = StartCoroutine(SmoothSlider(value));
        }

        private void SetSliderValue(float value)
        {
            if (value > _slider.maxValue)
            {
                value = _slider.maxValue;
            }

            _slider.value = value;
        }

        private void SetPercent(float value)
        {
            string percent = string.Format(PercentText, (int)(value / _slider.maxValue * 100));
            _percentText.text = percent;
        }

        private IEnumerator SmoothSlider(float value)
        {
            float startValue = _slider.value;
            float endValue = value;

            float currentTime = _slider.value;

            while (currentTime < SmoothTime)
            {
                currentTime += Time.deltaTime;
                _slider.value = Mathf.Lerp(startValue, endValue, currentTime / SmoothTime);
                SetPercent(_slider.value);
                yield return null;
            }

            _slider.value = endValue;
            SetPercent(endValue);
        }
    }
}
