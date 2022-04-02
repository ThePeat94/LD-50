using System;
using EventArgs;
using Nidavellir;
using Nidavellir.Utils;
using UnityEngine;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_gameOverScreenPanel;
        [SerializeField] private GameObject m_gameHudPanel;

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