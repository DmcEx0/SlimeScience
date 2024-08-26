using System.Collections;
using SlimeScience.Characters;
using SlimeScience.Characters.Ship;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Ad
{
    public class CallShip : MonoBehaviour
    {
        private const int ReloadTimeInSeconds = 180;

        [SerializeField] private Button _callShipButton;
        [SerializeField] private Image _timerImage;

        private Ship _ship;
        private Advertisment _advertisment;
        private Player _player;
        private Coroutine _reloadCoroutine;

        private void OnEnable()
        {
            _callShipButton.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _callShipButton.onClick.RemoveListener(OnClicked);
        }

        public void Init(Ship ship, Advertisment advertisment, Player player)
        {
            _ship = ship;
            _advertisment = advertisment;
            _player = player;
            _callShipButton.interactable = true;
            _timerImage.fillAmount = 0f;
        }

        private void OnClicked()
        {
            _advertisment.ShowReward();

            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }

            _reloadCoroutine = StartCoroutine(Reload());
        }

        private IEnumerator Reload()
        {
            _callShipButton.interactable = false;
            _timerImage.fillAmount = 1f;

            float time = ReloadTimeInSeconds;

            while (time > 0)
            {
                time -= Time.deltaTime;
                _timerImage.fillAmount = time / ReloadTimeInSeconds;
                yield return null;
            }

            _callShipButton.interactable = true;
        }
    }
}
