using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private Transform m_projectileSpawn;
        private Animator m_animator;
        private CharacterController m_characterController;
        private GameObject m_currentInteractable;
        private InputProcessor m_inputProcessor;

        private Vector3 m_moveDirection;

        public static PlayerController Instance { get; private set; }


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
            this.m_characterController = this.GetComponent<CharacterController>();
            this.m_animator = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            this.Move();
            this.TiltSpaceship();
            this.CheckShoot();
        }


        private void CheckShoot()
        {
            if (!this.m_inputProcessor.ShootTriggered)
                return;

            this.Shoot();
        }

        private void Shoot()
        {
            var instantiated = Instantiate(this.m_playerData.Projectile, this.m_projectileSpawn.position, Quaternion.identity);
            instantiated.GetComponent<Rigidbody>()
                .AddForce(Vector3.forward * 50, ForceMode.Impulse);
        }

        private void TiltSpaceship()
        {
        }

        protected void Move()
        {
            this.m_moveDirection = new Vector3(this.m_inputProcessor.Movement.x, 0, this.m_inputProcessor.Movement.y);
            this.m_characterController.Move(this.m_moveDirection * Time.deltaTime * this.m_playerData.MovementSpeed);
        }
    }
}