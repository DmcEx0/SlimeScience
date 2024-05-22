using SlimeScience.Input;
using SlimeScience.Leaderbords;
using SlimeScience.Money;
using SlimeScience.Saves;
using SlimeScience.Upgrades;
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

        [SerializeField] private Button _openUpgradesCanvas;
        [SerializeField] private Button _openLeaderbordCanvas;

        private void OnEnable()
        {
            if(_openUpgradesCanvas != null)
            {
                _openUpgradesCanvas.onClick.AddListener(OnOpenUpgradesClicked);
            }

            if(_upgradesCanvas != null)
            {
                _upgradesCanvas.Closed += OnOpenUpgradesClosed;
            }

            if(_openLeaderbordCanvas != null)
            {
                _openLeaderbordCanvas.onClick.AddListener(OnOpendLeaderbord);
            }

            if (_leaderbordCanvas != null)
            {
                _leaderbordCanvas.Closed += OnLeaderbordClosed;
            }
        }

        private void OnDisable()
        {
            if(_openUpgradesCanvas != null)
            {
                _openUpgradesCanvas.onClick.RemoveListener(OnOpenUpgradesClicked);
            }

            if(_upgradesCanvas != null)
            {
                _upgradesCanvas.Closed -= OnOpenUpgradesClosed;
            }

            if(_openLeaderbordCanvas != null)
            {
                _openLeaderbordCanvas.onClick.RemoveListener(OnOpendLeaderbord);
            }

            if (_leaderbordCanvas != null)
            {
                _leaderbordCanvas.Closed -= OnLeaderbordClosed;
            }
        }

        public void Init(Wallet wallet, GameVariables gameVariables)
        {
            _floatingJoystick.Init();
            _walletRenderer.Init(wallet);
            _upgradesCanvas.Init(wallet, gameVariables);
            _leaderbordCanvas.Init();

            // _openUpgradesCanvas.onClick.AddListener(OnOpenUpgradesClicked);
            //_upgradesCanvas.Closed += OnOpenUpgradesClosed;
        }

        private void OnOpenUpgradesClicked()
        {
            _openUpgradesCanvas.gameObject.SetActive(false);
            _upgradesCanvas.Show();
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
        }

        private void OnLeaderbordClosed()
        {
            _openLeaderbordCanvas.gameObject.SetActive(true);
            _leaderbordCanvas.gameObject.SetActive(false);
        }
    }
}
