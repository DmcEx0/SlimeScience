using System;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Tutorial
{
    public class TutorialImage : MonoBehaviour
    {
        private const int MinCurrentNumber = 0;
        
        [SerializeField] private Button _nextTutorialButton;
        [SerializeField] private Button _previousTutorialButton;
        [SerializeField] private Button _readyButton;
        
        [SerializeField] private Image[] _tutorials;

        private Image _currentTutorial;
        private int _currentImageNumber = 0;

        public event Action PopupClosed; 

        private void OnEnable()
        {
            _readyButton.gameObject.SetActive(false);

            foreach (var tutorial in _tutorials)
            {
                tutorial.gameObject.SetActive(false);
            }

            _currentTutorial = _tutorials[_currentImageNumber];
            _currentTutorial.gameObject.SetActive(true);

            ActivateButton(_currentImageNumber);
            
            _nextTutorialButton.onClick.AddListener(ShowNextTutorial);
            _previousTutorialButton.onClick.AddListener(ShowPreviousTutorial);
            _readyButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _nextTutorialButton.onClick.RemoveListener(ShowNextTutorial);
            _previousTutorialButton.onClick.RemoveListener(ShowPreviousTutorial);
            _readyButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            gameObject.SetActive(false);
            PopupClosed?.Invoke();
        }
        
        private void ShowNextTutorial()
        {
            int positiveIndex = 1;
            EnableTutorial(positiveIndex);
        }

        private void ShowPreviousTutorial()
        {
            int negativeIndex = -1;
            EnableTutorial(negativeIndex);
        }
        
        private void EnableTutorial(int index)
        {
            _currentImageNumber = Mathf.Clamp(_currentImageNumber + index, MinCurrentNumber, _tutorials.Length - 1);

            ActivateButton(_currentImageNumber);

            _currentTutorial.gameObject.SetActive(false);
            _currentTutorial = _tutorials[_currentImageNumber];
            _currentTutorial.gameObject.SetActive(true);
        }
        
        private void ActivateButton(int currentNumber)
        {
            _readyButton.gameObject.SetActive(false);
            _previousTutorialButton.gameObject.SetActive(true);
            _nextTutorialButton.gameObject.SetActive(true);

            if (currentNumber == MinCurrentNumber)
            {
                _previousTutorialButton.gameObject.SetActive(false);
            }

            if (currentNumber == _tutorials.Length - 1)
            {
                _nextTutorialButton.gameObject.SetActive(false);
                _readyButton.gameObject.SetActive(true);
            }
        }
    }
}
