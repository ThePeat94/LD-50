using UnityEngine;

namespace Nidavellir
{
    public class Asteroid : MonoBehaviour, IHittable
    {
        [SerializeField] private int m_shieldDamage;
        [SerializeField] private float m_slowDownFactor;
        [SerializeField] private float m_speedUpFactor;


        [SerializeField] private RandomClipPlayer m_randomExplodePlayer;
        [SerializeField] private RandomClipPlayer m_randomPlayerHitPlayer;

        private AsteroidFeatureHandler m_asteroidFeatureHandler;

        public float SlowDownFactor => this.m_slowDownFactor;
        public int Damage => this.m_shieldDamage;
        public float SpeedUpFactor => this.m_speedUpFactor;

        private void Awake()
        {
            this.m_asteroidFeatureHandler = FindObjectOfType<AsteroidFeatureHandler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out _))
            {
                this.DisableMesh();
                this.m_randomPlayerHitPlayer.PlayRandomOneShot();
                this.m_asteroidFeatureHandler.HandleAsteroidCollision(this);
                Destroy(this.gameObject, 2f);
            }
        }

        public void Hit()
        {
            this.DisableMesh();
            this.m_randomExplodePlayer.PlayRandomOneShot();
            this.m_asteroidFeatureHandler.DestroyedAsteroid(this);
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