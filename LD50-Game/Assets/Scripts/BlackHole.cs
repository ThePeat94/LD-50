using System;
using EventArgs;
using Nidavellir.Utils;
using UnityEngine;

namespace Nidavellir
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        [SerializeField] private GameObject m_distanceStartPoint;
        [SerializeField] private float m_slowDangerDistance;
        [SerializeField] private float m_fastDangerDistance;
        [SerializeField] private Transform m_sphere;


        private Vector3 m_constantForce;
        private GameStateManager m_gameStateManager;
        private Rigidbody m_rigidbody;

        public Transform Sphere => this.m_sphere;

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
            if (this.m_gameStateManager.CurrentState != GameState.Started)
                return;

            this.m_rigidbody.velocity = this.m_constantForce;

            var distance = Mathf.Abs(Vector3.Distance(this.m_distanceStartPoint.transform.position, PlayerController.Instance.transform.position));

            if (distance <= this.m_fastDangerDistance)
                DangerMusicPlayer.Instance.PlayFastDanger();
            else if (distance <= this.m_slowDangerDistance)
                DangerMusicPlayer.Instance.PlaySlowDanger();
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
                    DangerMusicPlayer.Instance.Stop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}