using System;
using EventArgs;
using Nidavellir.ResourceControllers;
using Nidavellir.Utils;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] private BlackHoleData m_data;
        [SerializeField] private GameObject m_distanceStartPoint;
        [SerializeField] private Transform m_sphere;


        private Vector3 m_constantForce;
        private FuelResourceController m_fuelResourceController;
        private GameStateManager m_gameStateManager;
        private Rigidbody m_rigidbody;
        private float m_speed;

        public Transform Sphere => this.m_sphere;
        public float Speed => this.m_rigidbody.velocity.magnitude;

        private void Awake()
        {
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
            this.m_gameStateManager.GameStateChanged += this.OnGameStateChanged;
            this.m_speed = this.m_data.Speed;
        }

        private void Start()
        {
            this.m_rigidbody = this.GetComponent<Rigidbody>();
            FindObjectOfType<FuelResourceController>()
                .ResourceController.ResourceValueChanged += this.OnFuelChanged;
        }

        private void Update()
        {
            if (this.m_gameStateManager.CurrentState != GameState.Started)
                return;

            this.m_rigidbody.velocity = this.m_constantForce;

            var distance = Mathf.Abs(Vector3.Distance(this.m_distanceStartPoint.transform.position, PlayerController.Instance.transform.position));

            if (distance <= this.m_data.FastDangerDistance)
                DangerMusicPlayer.Instance.PlayFastDanger();
            else if (distance <= this.m_data.SlowDangerDistance)
                DangerMusicPlayer.Instance.PlaySlowDanger();

            if (distance >= this.m_data.SlingDistance)
            {
                Debug.Log("Sling sling");
                this.transform.position = Vector3.Lerp(this.transform.position, PlayerController.Instance.transform.position, this.m_data.SlingStrength * Time.deltaTime);
            }
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
                this.EffectVelocity(0.1f);
                Destroy(other.transform.parent.gameObject);
            }
        }

        public void EffectVelocity(float effect)
        {
            var result = this.m_speed + effect;
            this.m_speed = Math.Max(result, this.m_data.MinSpeed);

            this.m_constantForce = Vector3.forward * this.m_speed;
        }

        private void OnFuelChanged(object sender, ResourceValueChangedEventArgs e)
        {
            if (e.NewValue <= 0.1f)
                this.EffectVelocity(0.5f * Mathf.Abs(Vector3.Distance(this.m_distanceStartPoint.transform.position, PlayerController.Instance.transform.position)));
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