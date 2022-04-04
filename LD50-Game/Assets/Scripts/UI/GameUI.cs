using System;
using EventArgs;
using Nidavellir;
using Nidavellir.Utils;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_gameOverScreenPanel;
        [SerializeField] private GameObject m_gameHudPanel;
        [SerializeField] private TextMeshProUGUI m_gameOverText;

        private void Awake()
        {
            this.m_gameOverScreenPanel.SetActive(false);
            this.m_gameHudPanel.SetActive(true);
            FindObjectOfType<GameStateManager>()
                .GameStateChanged += this.OnGameStateChanged;
        }

        public void ShowGameHudPanel()
        {
            this.m_gameHudPanel.SetActive(true);
        }

        public void ShowGameOverScreen()
        {
            this.m_gameOverScreenPanel.SetActive(true);
            this.m_gameHudPanel.SetActive(false);
            this.m_gameOverText.text =
                $"In my last lightseconds in front of the black hole I decided to eject my black box.{Environment.NewLine}{Environment.NewLine}Goodbye World, I am sorry, I could not fulfil the mission for our greater needs.{Environment.NewLine}{Environment.NewLine}" +
                $"During my mission I travelled {PlayerController.Instance.PassedUnits:F0} Lightseconds within {LevelTimer.Instance.PastTimeSinceStart:mm\\:ss\\.ff} Lightseconds.";
        }

        private void OnGameStateChanged(object sender, GameStateChangedEventArgs args)
        {
            switch (args.NewGameState)
            {
                case GameState.Started:
                    this.ShowGameHudPanel();
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    this.ShowGameOverScreen();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}