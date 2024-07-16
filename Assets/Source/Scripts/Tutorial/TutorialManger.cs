using SlimeScience.Blocks;
using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.Saves;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SlimeScience.Tutorial
{
    public class TutorialManger : MonoBehaviour
    {
        private const string GameSceneName = "Game";
        [SerializeField] private ReleaseZone _releaseZone;
        
        [SerializeField] private Image _fade;
        [SerializeField] private TutorialImage _welcomePopup;
        [SerializeField] private TutorialImage _trigger1Popup;
        [SerializeField] private TutorialImage _releaseSlimesPopup;
        [SerializeField] private TutorialImage _upgradePopup;
        [SerializeField] private TutorialImage _nextBlockOpen;

        [Space, Header("Triggers")]
        [SerializeField] private Collider _trigger1;

        private GameVariables _gameVariables;
        private Player _player;
        private int _currentSlimesAmount;

        private void Start()
        {
            _releaseZone.Collider.enabled = false;
            
            _welcomePopup.PopupClosed += CloseFadeScreen;
            _trigger1Popup.PopupClosed += CloseFadeScreen;
            _releaseSlimesPopup.PopupClosed += CloseFadeScreen;
            _upgradePopup.PopupClosed += CloseFadeScreen;
            _nextBlockOpen.PopupClosed += CloseFadeScreen;
        }

        private void OnDestroy()
        {
            _welcomePopup.PopupClosed -= CloseFadeScreen;
            _trigger1Popup.PopupClosed -= CloseFadeScreen;
            _releaseSlimesPopup.PopupClosed -= CloseFadeScreen;
            _upgradePopup.PopupClosed -= CloseFadeScreen;
        }

        public void Init(Player player, GameVariables gameVariables)
        {
            _gameVariables = gameVariables;
            _player = player;
            _player.PullGun.Catched += ShowReleasePopup;
            _releaseZone.PlayerReleased += ShowUpgradePopup;
            
            OpenFadeScreen();
            _welcomePopup.gameObject.SetActive(true);
        }

        public void ShowPopupForTrigger1()
        {
            OpenFadeScreen();
            _trigger1Popup.gameObject.SetActive(true);
            _trigger1.enabled = false;
        }

        public void GoToGameScene()
        {
            SceneManager.LoadScene(GameSceneName);
        }
        
        private void ShowReleasePopup()
        {
            _currentSlimesAmount++;
            
            if(_currentSlimesAmount == (int)_gameVariables.AbsorptionCapacity)
            {
                OpenFadeScreen();
                _releaseSlimesPopup.gameObject.SetActive(true);
                
                _releaseZone.Collider.enabled = true;
                
                _player.PullGun.Catched -= ShowReleasePopup;
            }
        }

        private void ShowUpgradePopup()
        {
            OpenFadeScreen();
            _upgradePopup.gameObject.SetActive(true);
            
            _releaseZone.PlayerReleased -= ShowUpgradePopup;
            _releaseZone.OpenedNextBlock += ShowOpenNextBlockPopup;
        }

        private void ShowOpenNextBlockPopup(BlockData nextBlock, int value)
        {
            OpenFadeScreen();
            _nextBlockOpen.gameObject.SetActive(true);
            
            _releaseZone.OpenedNextBlock -= ShowOpenNextBlockPopup;
        }

        private void CloseFadeScreen()
        {
            SetPlayerMovementState(true);
            _fade.gameObject.SetActive(false);
        }

        private void OpenFadeScreen()
        {
            SetPlayerMovementState(false);
            _fade.gameObject.SetActive(true);
        }
        
        private void SetPlayerMovementState(bool state)
        {
            if(state)
            {
                _player.Movement.Enable();
                return;
            }
            
            _player.Movement.Disable();
        }
    }
}