using System;
using DG.Tweening;
using SlimeScience.Blocks;
using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.Saves;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private TutorialImage _trigger2Popup;
        [SerializeField] private TutorialImage _releaseSlimesPopup;
        [SerializeField] private TutorialImage _upgradePopup;
        [SerializeField] private TutorialImage _nextBlockOpen;

        [Space, Header("Triggers")]
        [SerializeField] private Collider _trigger1;

        private Tweener _fadeTweener;
        
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
            _trigger2Popup.PopupClosed += OnFinishTutorPopupClosed;
        }

        private void OnDestroy()
        {
            _welcomePopup.PopupClosed -= CloseFadeScreen;
            _trigger1Popup.PopupClosed -= CloseFadeScreen;
            _releaseSlimesPopup.PopupClosed -= CloseFadeScreen;
            _upgradePopup.PopupClosed -= CloseFadeScreen;
            _nextBlockOpen.PopupClosed -= CloseFadeScreen;
            _trigger2Popup.PopupClosed -= OnFinishTutorPopupClosed;
        }

        public void Init(Player player, GameVariables gameVariables)
        {
            _gameVariables = gameVariables;
            _player = player;
            _player.PullGun.Catched += ShowReleasePopup;
            _releaseZone.PlayerReleased += ShowUpgradePopup;
            
            OpenFadeScreen();
            _welcomePopup.Show();
        }

        public void ShowPopupForTrigger1()
        {
            OpenFadeScreen();
            _trigger1Popup.Show();
            _trigger1.enabled = false;
        }
        public void ShowPopupForTrigger2()
        {
            OpenFadeScreen();
            _trigger2Popup.Show();
        }

        public void GoToGameScene()
        {
            _gameVariables.Save();
            SceneManager.LoadScene(GameSceneName);
        }
        
        private void ShowReleasePopup()
        {
            _currentSlimesAmount++;
            
            if(_currentSlimesAmount == (int)_gameVariables.AbsorptionCapacity)
            {
                OpenFadeScreen();
                _releaseSlimesPopup.Show();
                
                _releaseZone.Collider.enabled = true;
                
                _player.PullGun.Catched -= ShowReleasePopup;
            }
        }

        private void ShowUpgradePopup()
        {
            OpenFadeScreen();
            _upgradePopup.Show();
            
            _releaseZone.PlayerReleased -= ShowUpgradePopup;
            _releaseZone.OpenedNextBlock += ShowOpenNextBlockPopup;
        }

        private void ShowOpenNextBlockPopup(BlockData nextBlock, int value)
        {
            OpenFadeScreen();
            _nextBlockOpen.Show();
            
            _releaseZone.OpenedNextBlock -= ShowOpenNextBlockPopup;
        }

        private void CloseFadeScreen()
        {
            SetPlayerMovementState(true);
            // _fadeTweener?.Kill();
            // _fade.material.DOFade(0f, 0.3f).SetEase(Ease.Linear)
            //     .OnComplete(() =>
            //     {
            //         _fade.gameObject.SetActive(false);
            //     });
            
            _fade.gameObject.SetActive(false);
        }

        private void OpenFadeScreen()
        {
            SetPlayerMovementState(false);
            _fade.gameObject.SetActive(true);

            // _fadeTweener?.Kill();
            // _fade.material.DOFade(1f, 0.3f).SetEase(Ease.Linear);
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
        
        private void OnFinishTutorPopupClosed()
        {
            PlayerPrefs.DeleteAll();
            _gameVariables.ResetSave();
            PlayerPrefs.SetInt("Tutorial", 1);
            GoToGameScene();
        }
    }
}