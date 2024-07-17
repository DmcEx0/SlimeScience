using SlimeScience.Ad;
using SlimeScience.Audio;
using SlimeScience.Input;
using SlimeScience.Leaderbords;
using SlimeScience.Money;
using SlimeScience.PauseSystem;
using SlimeScience.Saves;
using SlimeScience.Upgrades;
using SlimeScience.Util;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Root
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick _floatingJoystick;
        [SerializeField] private WalletRenderer _walletRenderer;
        [SerializeField] private UpgradesCanvas _upgradesCanvas;
        [SerializeField] private LeaderbordCanvas _leaderbordCanvas;
        [SerializeField] private AdvertismentCanvas _advertismentCanvas;
        [SerializeField] private AudioChanger _audioChanger;
        [SerializeField] private ProgressRenderer _progressRenderer;
        [SerializeField] private ShipPopup _shipPopup;
        
        [SerializeField] private Button _openUpgradesCanvas;
        [SerializeField] private Button _openLeaderbordCanvas;

        [SerializeField] private Button _resetSave;

        private Advertisment _advertisment;
        private PauseHandler _adPause;
        private GameVariables _gameVariables;

        private void OnEnable()
        {
            if (_openUpgradesCanvas != null)
            {
                _openUpgradesCanvas.onClick.AddListener(OnOpenUpgradesClicked);
            }

            if (_upgradesCanvas != null)
            {
                _upgradesCanvas.Closed += OnOpenUpgradesClosed;
            }

            if (_openLeaderbordCanvas != null)
            {
                _openLeaderbordCanvas.onClick.AddListener(OnOpendLeaderbord);
            }

            if (_leaderbordCanvas != null)
            {
                _leaderbordCanvas.Closed += OnLeaderbordClosed;
            }

            if (_advertismentCanvas != null)
            {
                _advertismentCanvas.Ended += OnAdPopupEnded;
            }

            if (_resetSave != null)
            {
                _resetSave.onClick.AddListener(OnResetSaves);
            }

            _shipPopup.AdShowing += OnAdPopupEnded;
        }

        private void OnDisable()
        {
            if (_openUpgradesCanvas != null)
            {
                _openUpgradesCanvas.onClick.RemoveListener(OnOpenUpgradesClicked);
            }

            if (_upgradesCanvas != null)
            {
                _upgradesCanvas.Closed -= OnOpenUpgradesClosed;
            }

            if (_openLeaderbordCanvas != null)
            {
                _openLeaderbordCanvas.onClick.RemoveListener(OnOpendLeaderbord);
            }

            if (_leaderbordCanvas != null)
            {
                _leaderbordCanvas.Closed -= OnLeaderbordClosed;
            }

            if (_advertismentCanvas != null)
            {
                _advertismentCanvas.Ended -= OnAdPopupEnded;
            }

            if (_resetSave != null)
            {
                _resetSave.onClick.RemoveListener(OnResetSaves);
            }
            
            _shipPopup.AdShowing -= OnAdPopupEnded;
        }

        public void Init(
            Wallet wallet,
            GameVariables gameVariables,
            Advertisment advertisment,
            PauseHandler adPause)
        {
            _gameVariables = gameVariables;

            _advertisment = advertisment;
            _adPause = adPause;
            _floatingJoystick.Init();
            _walletRenderer.Init(wallet);
            _upgradesCanvas.Init(wallet, gameVariables);
            _leaderbordCanvas.Init();
            _audioChanger.Init();
            _progressRenderer.Init(gameVariables);
        }

        public void ShowInterstitial()
        {
            if (_advertismentCanvas.Available == false)
            {
                return;
            }

            _adPause.Pause();
            _advertismentCanvas.ShowPopup();
        }

        private void OnOpenUpgradesClicked()
        {
            _openUpgradesCanvas.gameObject.SetActive(false);
            _upgradesCanvas.Show();
            SoundsManager.PlayTapUI();
        }

        private void OnOpenUpgradesClosed()
        {
            _openUpgradesCanvas.gameObject.SetActive(true);
            _upgradesCanvas.gameObject.SetActive(false);
        }

        private void OnOpendLeaderbord()
        {
            _openLeaderbordCanvas.gameObject.SetActive(false);
            _leaderbordCanvas.Open();
            SoundsManager.PlayTapUI();
        }

        private void OnLeaderbordClosed()
        {
            _openLeaderbordCanvas.gameObject.SetActive(true);
            _leaderbordCanvas.gameObject.SetActive(false);
        }

        private void OnAdPopupEnded()
        {
            _advertisment.Show();
        }

        private void OnResetSaves()
        {
            _gameVariables.ResetSave();
        }
    }
}