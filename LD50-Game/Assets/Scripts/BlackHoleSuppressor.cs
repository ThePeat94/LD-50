using UnityEngine;

namespace Nidavellir
{
    public class BlackHoleSuppressor : MonoBehaviour
    {
        [SerializeField] private BlackHole m_blackHole;

        private float m_currentSlowDown;

        private int m_destructionCount;

        public void AsteroidDestroyed(Asteroid destroyed)
        {
            this.m_destructionCount++;
            this.m_currentSlowDown -= destroyed.SlowDownFactor;

            if (this.m_destructionCount % 5 == 0)
            {
                Debug.Log($"Slowed black hole down by {this.m_currentSlowDown}");
                this.m_blackHole.EffectVelocity(this.m_currentSlowDown);
                this.m_destructionCount = 0;
                this.m_currentSlowDown = 0f;
            }
        }
    }
}