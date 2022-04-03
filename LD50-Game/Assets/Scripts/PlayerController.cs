using System;
using System.Collections;
using EventArgs;
using Nidavellir.ResourceControllers;
using Nidavellir.Utils;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private Transform m_projectileSpawn;
        [SerializeField] private Transform m_modelTransform;

        private readonly float m_maxTiltAngle = 30f;
        private readonly float m_maxTiltTime = 0.66f;

        private Animator m_animator;
        private GameObject m_currentInteractable;
        private float m_elapsedTiltTime;

        private FuelResourceController m_fuelResourceController;
        private GameStateManager m_gameStateManager;
        private InputProcessor m_inputProcessor;
        private int m_lastTiltDir;

        private Vector3 m_moveDirection;
        private PlayerStatsManager m_playerStatsManager;

        private Rigidbody m_rigidbody;
        private Vector2 m_screenBounds;
        private Coroutine m_tiltCoroutine;

        public static PlayerController Instance { get; private set; }

        public float Velocity => this.m_rigidbody.velocity.magnitude;

        public float PassedUnits { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_rigidbody = this.GetComponent<Rigidbody>();
            this.m_animator = this.GetComponent<Animator>();
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
            this.m_gameStateManager.GameStateChanged += this.OnGameStateChanged;
            this.m_fuelResourceController = this.GetComponent<FuelResourceController>();
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
        }

        private void Start()
        {
            this.m_screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));
        }

        private void Update()
        {
            if (this.m_gameStateManager.CurrentState != GameState.Started)
                return;

            if (this.m_fuelResourceController.CanNavigate)
                this.m_moveDirection = new Vector3(this.m_inputProcessor.Movement.x, 0, this.m_inputProcessor.Movement.y);
            else
                this.m_moveDirection = Vector3.zero;


            this.CheckShoot();
            if (this.m_rigidbody.velocity.z > 0)
                this.PassedUnits += this.Velocity * Time.deltaTime;
            else if (this.m_rigidbody.velocity.z < 0)
                this.PassedUnits -= this.Velocity * Time.deltaTime;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (this.m_gameStateManager.CurrentState != GameState.Started)
                return;

            if (this.m_fuelResourceController.CanNavigate)
                this.Move();
            else
                this.m_rigidbody.velocity = Vector3.zero;

            this.DetermineTilt();
        }

        public void Die()
        {
        }

        private void CheckShoot()
        {
            if (!this.m_inputProcessor.ShootTriggered)
                return;

            this.Shoot();
        }

        private void DetermineTilt()
        {
            var startRotation = this.m_modelTransform.rotation;
            var newTiltDir = this.m_moveDirection.x switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => 0
            };

            var targetRotation = Quaternion.Euler(0f, 180f, this.m_maxTiltAngle * newTiltDir);

            if (this.m_lastTiltDir != newTiltDir)
            {
                if (this.m_tiltCoroutine != null)
                    this.StopCoroutine(this.m_tiltCoroutine);

                this.m_tiltCoroutine = this.StartCoroutine(this.Tilt(startRotation, targetRotation));
            }

            this.m_lastTiltDir = newTiltDir;
        }

        private void OnGameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            switch (e.NewGameState)
            {
                case GameState.Started:
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    this.m_rigidbody.velocity = Vector3.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Shoot()
        {
            var instantiated = Instantiate(this.m_playerData.Projectile, this.m_projectileSpawn.position, Quaternion.identity);
            instantiated.GetComponent<Rigidbody>()
                .AddForce(Vector3.forward * 50, ForceMode.Impulse);
        }

        private IEnumerator Tilt(Quaternion start, Quaternion end)
        {
            var elapsedTime = 0f;
            var t = elapsedTime / this.m_maxTiltTime;

            while (t < 1)
            {
                this.m_modelTransform.rotation = Quaternion.Lerp(start, end, t);
                elapsedTime += Time.fixedDeltaTime;
                t = elapsedTime / this.m_maxTiltTime;
                yield return new WaitForFixedUpdate();
            }

            this.m_tiltCoroutine = null;
        }

        protected void Move()
        {
            this.m_rigidbody.AddForce(this.m_moveDirection * this.m_playerStatsManager.PlayerStats.Acceleration, ForceMode.Acceleration);
            var velocity = Vector3.ClampMagnitude(this.m_rigidbody.velocity, this.m_playerStatsManager.PlayerStats.MaxMovementSpeed);
            this.m_rigidbody.velocity = velocity;
        }
    }
}