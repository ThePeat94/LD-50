using UnityEngine;

namespace Nidavellir
{
    public class InputProcessor : MonoBehaviour
    {
        private PlayerInput m_playerInput;

        public Vector2 Movement { get; private set; }

        public bool InteractTriggered => this.m_playerInput.Actions.Interact.triggered;
        public bool ShootTriggered => this.m_playerInput.Actions.Shoot.triggered;

        private void Awake()
        {
            this.m_playerInput = new PlayerInput();
        }

        private void Update()
        {
            this.Movement = this.m_playerInput.Actions.Move.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            this.m_playerInput?.Enable();
        }

        private void OnDisable()
        {
            this.m_playerInput?.Disable();
            this.Movement = Vector3.zero;
        }
    }
}