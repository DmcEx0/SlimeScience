using SlimeScience.Util;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Audio
{
    public class AudioChanger : MonoBehaviour
    {
        private const string Audio = "Audio";
        private const int TurnOn = 1;
        private const int TurnOff = 0;

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _turnOn;
        [SerializeField] private Sprite _turnOff;
        [SerializeField] private Button _button;

        private bool _isOn = true;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnChangeClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnChangeClicked);
        }

        public void Init()
        {
            _isOn = PlayerPrefs.GetInt(Audio) == TurnOn;
            _image.sprite = _isOn ? _turnOn : _turnOff;

            if (_isOn)
            {
                SoundsManager.TurnOn();
                return;
            }

            SoundsManager.TurnOff();
        }

        public void Change()
        {
            if (_isOn)
            {
                Disable();
            }
            else
            {
                Enable();
            }

            _isOn = !_isOn;
        }

        private void Enable()
        {
            _image.sprite = _turnOn;
            PlayerPrefs.SetInt(Audio, TurnOn);
            SoundsManager.TurnOn();
        }

        private void Disable()
        {
            _image.sprite = _turnOff;
            PlayerPrefs.SetInt(Audio, TurnOff);
            SoundsManager.TurnOff();
        }

        private void OnChangeClicked()
        {
            Change();
        }
    }
}
