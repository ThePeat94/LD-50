﻿using System;
using EventArgs;
using Nidavellir.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private AudioClip m_gameOverTheme;

        private EventHandler<GameStateChangedEventArgs> m_gameStateChanged;
        private InputProcessor m_inputProcessor;

        public GameState CurrentState { get; private set; } = GameState.Paused;

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged
        {
            add => this.m_gameStateChanged += value;
            remove => this.m_gameStateChanged -= value;
        }

        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
        }

        private void Start()
        {
            this.TriggerGameStarted();
        }

        private void Update()
        {
#if UNITY_STANDALONE
            if (this.m_inputProcessor.QuitTriggered)
            {
                Application.Quit();
                return;
            }
#endif
            if (this.m_inputProcessor.BackToMainTriggered)
            {
                SceneManager.LoadScene(0);
                return;
            }

            if (this.CurrentState == GameState.GameOver && this.m_inputProcessor.ShootTriggered)
            {
                // TODO: CHANGE ME!!!!
                SceneManager.LoadScene(0);
            }
        }

        public void TriggerGameOver()
        {
            this.TriggerGameState(GameState.GameOver);
            LevelTimer.Instance.StopStopWatch();
            MusicPlayer.Instance.PlayMusicOnce(this.m_gameOverTheme);
        }

        private void TriggerGameStarted()
        {
            this.TriggerGameState(GameState.Started);
            LevelTimer.Instance.RestartStopWatch();
        }

        private void TriggerGameState(GameState newState)
        {
            if (this.CurrentState == newState)
                return;

            this.CurrentState = newState;
            this.m_gameStateChanged?.Invoke(this, new GameStateChangedEventArgs(newState));
        }
    }
}