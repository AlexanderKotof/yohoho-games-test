using System;
using Test.Data;
using TMPro;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Test.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startGameScreen;
        [SerializeField] private Button _startGameButton;

        [SerializeField] private GameObject _inGameScreen;
        [SerializeField] private JoystickComponent _joystick;
        [SerializeField] private TMP_Text coinsText;

        public JoystickComponent Joystick => _joystick;

        public void ShowStartGameScreen(Action onStartButtonClick)
        {
            _startGameScreen.SetActive(true);
            _startGameButton.onClick.AddListener(onStartButtonClick.Invoke);
        }

        internal void HideStartScreen()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _startGameScreen.SetActive(false);
        }

        public void ShowInGameScreen()
        {
            _inGameScreen.SetActive(true);
        }

        public void SetCoins(int value)
        {
            coinsText.SetText($"${value}");
        }
    }
}