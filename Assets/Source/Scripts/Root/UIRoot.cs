using SlimeScience.Input;
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

        [SerializeField] private Button _openUpgradesCanvas;

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
        }

        public void Init(Wallet wallet, GameVariables gameVariables)
        {
            _floatingJoystick.Init();
            _walletRenderer.Init(wallet);
            _upgradesCanvas.Init(wallet, gameVariables);

            _openUpgradesCanvas.onClick.AddListener(OnOpenUpgradesClicked);
            _upgradesCanvas.Closed += OnOpenUpgradesClosed;
        }

        private void OnOpenUpgradesClicked()
        {
            _openUpgradesCanvas.gameObject.SetActive(false);
            _upgradesCanvas.gameObject.SetActive(true);
        }

        private void OnOpenUpgradesClosed()
        {
            _openUpgradesCanvas.gameObject.SetActive(true);
            _upgradesCanvas.gameObject.SetActive(false);
        }
    }
}
