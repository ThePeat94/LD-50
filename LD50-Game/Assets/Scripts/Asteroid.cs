using UnityEngine;

namespace Nidavellir
{
    public class Asteroid : MonoBehaviour, IHittable
    {
        [SerializeField] private int m_shieldDamage;
        [SerializeField] private float m_slowDownFactor;

        [SerializeField] private RandomClipPlayer m_randomExplodePlayer;
        [SerializeField] private RandomClipPlayer m_randomPlayerHitPlayer;
        private BlackHoleSuppressor m_blackHoleSuppressor;

        public float SlowDownFactor => this.m_slowDownFactor;

        private void Awake()
        {
            this.m_blackHoleSuppressor = FindObjectOfType<BlackHoleSuppressor>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ShieldController>(out var shieldController))
            {
                shieldController.InflictDamage(this.m_shieldDamage);
                this.DisableMesh();
                this.m_randomPlayerHitPlayer.PlayRandomOneShot();
                Destroy(this.gameObject, 2f);
            }
        }

        public void Hit()
        {
            this.DisableMesh();
            this.m_randomExplodePlayer.PlayRandomOneShot();
            this.m_blackHoleSuppressor.AsteroidDestroyed(this);
            Destroy(this.gameObject, 2f);
        }

        private void DisableMesh()
        {
            this.GetComponentInChildren<Collider>()
                .enabled = false;
            this.GetComponentInChildren<MeshRenderer>()
                .enabled = false;
        }
    }
}