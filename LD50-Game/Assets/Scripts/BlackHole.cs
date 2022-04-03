using System;
using EventArgs;
using Nidavellir.Utils;
using UnityEngine;

namespace Nidavellir
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        private Vector3 m_constantForce;
        private GameStateManager m_gameStateManager;
        private Rigidbody m_rigidbody;

        private void Awake()
        {
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
            this.m_gameStateManager.GameStateChanged += this.OnGameStateChanged;
        }

        private void Start()
        {
            this.m_rigidbody = this.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            this.m_rigidbody.velocity = this.m_constantForce;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (this.m_gameStateManager.CurrentState != GameState.Started)
                return;

            if (other.gameObject.TryGetComponent<PlayerController>(out var playerController))
            {
                playerController.Die();
                this.m_gameStateManager.TriggerGameOver();
                return;
            }

            if (other.GetComponentInParent<Asteroid>() != null)
            {
                this.m_constantForce.z += 0.1f;
                Destroy(other.transform.parent.gameObject);
            }
        }

        private void OnGameStateChanged(object sender, GameStateChangedEventArgs args)
        {
            switch (args.NewGameState)
            {
                case GameState.Started:
                    this.m_constantForce = Vector3.forward * this.m_speed;
                    break;
                case GameState.Paused:
                case GameState.GameOver:
                    this.m_constantForce = this.m_rigidbody.velocity = Vector3.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}