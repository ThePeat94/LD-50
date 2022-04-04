using UnityEngine;

namespace Nidavellir
{
    public class PlayerAsteroidTracker : MonoBehaviour
    {
        [SerializeField] private float m_maxSpeedEffect;


        private PlayerStatsManager m_playerStatsManager;

        public int DestroyedAmount { get; private set; }

        private void Awake()
        {
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
        }

        public void AsteroidDestroyed()
        {
            this.DestroyedAmount++;
            this.m_playerStatsManager.EffectWithGivenDelta(new PlayerStats
            {
                MaxMovementSpeed = 0.05f
            });
        }
    }
}