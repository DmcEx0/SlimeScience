using SlimeScience.Util;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Audio
{
    public class AudioChanger : MonoBehaviour
    {
        private const int TurnOn = 1;
        private const int TurnOff = 0;

        [SerializeField] private SoundType _type;
        
        [Space]
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
            _isOn = PlayerPrefs.GetInt(GetSoundKey(_type)) == TurnOn;
            _isOn = true;
            _image.sprite = _isOn ? _turnOn : _turnOff;

            Debug.Log(_isOn + " + " + _type);
            if (_isOn)
            {
                SoundsManager.TurnOn(_type);
                return;
            }

            SoundsManager.TurnOff(_type);
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
            PlayerPrefs.SetInt(GetSoundKey(_type), TurnOn);
            SoundsManager.TurnOn(_type);
        }

        private void Disable()
        {
            _image.sprite = _turnOff;
            PlayerPrefs.SetInt(GetSoundKey(_type), TurnOff);
            SoundsManager.TurnOff(_type);
        }

        private void OnChangeClicked()
        {
            Change();
        }
        
        private string GetSoundKey(SoundType type)
        {
            switch (type)
            {
                case SoundType.Background:
                    return SoundsManager.BackgroundKey;
                case SoundType.Sfx:
                    return SoundsManager.SfxKey;
            }

            return null;
        }
    }
}
