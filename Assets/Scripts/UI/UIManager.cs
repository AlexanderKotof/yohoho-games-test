using System;
using UnityEngine;
using UnityEngine.UI;

namespace Test.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startGameScreen;
        [SerializeField] private Button _startGameButton;

        [SerializeField] private GameObject _inGameScreen;
        [SerializeField] private GameObject _joystick;



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
    }
}