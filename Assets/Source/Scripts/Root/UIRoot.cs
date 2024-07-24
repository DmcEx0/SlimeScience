using SlimeScience.Ad;
using SlimeScience.Audio;
using SlimeScience.Characters;
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
        [SerializeField] private Ship _ship;
        [SerializeField] private FloatingJoystick _floatingJoystick;
        [SerializeField] private WalletRenderer _walletRenderer;
        [SerializeField] private UpgradesCanvas _upgradesCanvas;
        [SerializeField] private LeaderbordCanvas _leaderbordCanvas;
        [SerializeField] private AdvertismentCanvas _advertismentCanvas;
        [SerializeField] private AudioChanger _audioChanger;
        [SerializeField] private ProgressRenderer _progressRenderer;
        [SerializeField] private ShipPopup _shipPopup;

        [SerializeField] private CallShip _callShip;
        [SerializeField] private Button _openUpgradesCanvas;
        [SerializeField] private Button _openLeaderboardCanvas;

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

            if (_openLeaderboardCanvas != null)
            {
                _openLeaderboardCanvas.onClick.AddListener(OnOpendLeaderbord);
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

            if (_shipPopup != null)
            {
                _shipPopup.AdShowing += OnShowReward;
            }
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

            if (_openLeaderboardCanvas != null)
            {
                _openLeaderboardCanvas.onClick.RemoveListener(OnOpendLeaderbord);
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

            if (_shipPopup != null)
            {
                _shipPopup.AdShowing -= OnShowReward;
            }
            
            _advertisment.RewardClaimed -= OnRewardClaimed;
        }

        public void Init(
            Wallet wallet,
            GameVariables gameVariables,
            Advertisment advertisment,
            PauseHandler adPause,
            Player player)
        {
            _gameVariables = gameVariables;

            _advertisment = advertisment;
            _advertisment.RewardClaimed += OnRewardClaimed;
            _adPause = adPause;
            _floatingJoystick.Init();
            _walletRenderer.Init(wallet);
            _upgradesCanvas.Init(wallet, gameVariables);
            _leaderbordCanvas.Init();
            _audioChanger.Init();
            _progressRenderer.Init(gameVariables);
            
            _ship.Init();
            _callShip.Init(_ship, _advertisment, player);
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
            _openLeaderboardCanvas.gameObject.SetActive(false);
            _leaderbordCanvas.Open();
            SoundsManager.PlayTapUI();
        }

        private void OnLeaderbordClosed()
        {
            _openLeaderboardCanvas.gameObject.SetActive(true);
            _leaderbordCanvas.gameObject.SetActive(false);
        }

        private void OnAdPopupEnded()
        {
            _advertisment.Show();
        }
        
        private void OnShowReward()
        {
            _advertisment.ShowReward();
        }

        private void OnResetSaves()
        {
            _gameVariables.ResetSave();
        }

        private void OnRewardClaimed()
        {
            _ship.Used();
        }
    }
}