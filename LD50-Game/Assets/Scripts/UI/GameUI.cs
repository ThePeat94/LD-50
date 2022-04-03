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
            this.m_gameOverText.text = "Professor Brand, in the last few lightyears in front of the event horizon I decided to eject my blackbox. After trying to reach you for " +
                                       $"{PlayerController.Instance.PassedUnits:F2} Lightyears I could not keep distance to the black hole anymore. Every second of the {LevelTimer.Instance.PastTimeSinceStart:mm\\:ss\\.fff} " +
                                       $"long escape I tried to think of your last words, before I left earth to find another planet to live on:{Environment.NewLine}{Environment.NewLine}" +
                                       $"Do not go gentle into that good night,{Environment.NewLine}" + $"Old age should burn and rave at close of day;{Environment.NewLine}" +
                                       $"Rage, rage against the dying of the light.{Environment.NewLine}" + $"{Environment.NewLine}" +
                                       $"Though wise men at their end know dark is right,{Environment.NewLine}" + $"Because their words had forked no lightning they{Environment.NewLine}" +
                                       "Do not go gentle into that good night.";
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