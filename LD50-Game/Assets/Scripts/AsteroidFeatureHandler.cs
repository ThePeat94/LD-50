using System.Collections;
using EventArgs;
using UnityEngine;

namespace Nidavellir
{
    public class AsteroidFeatureHandler : MonoBehaviour
    {
        [SerializeField] private float m_activateAsteroidSlowDownAfterSeconds;
        [SerializeField] private float m_activateShieldSlowDownAfterSeconds;
        [SerializeField] private float m_increasePlayerSpeedAfterSeconds;
        private bool m_increaseSpeed;


        private PlayerStatsManager m_playerStatsManager;
        private ShieldController m_shieldController;

        private bool m_slowDownIfHit;
        private bool m_slowDownIfLostShield;

        private void Awake()
        {
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
            this.m_shieldController = this.GetComponent<ShieldController>();
        }

        private void Start()
        {
            this.StartCoroutine(this.ActivateIncreaseEffect());
            this.StartCoroutine(this.ActivateAsteroidSlowDownEffect());
            this.StartCoroutine(this.ActivateShieldSlowDownEffect());

            this.m_shieldController.ResourceController.ResourceValueChanged += this.OnShieldValueChanged;
        }

        public void DestroyedAsteroid(Asteroid destroyed)
        {
            if (this.m_increaseSpeed)
            {
                Debug.Log($"Speed up player by {destroyed.SpeedUpFactor}");
                this.m_playerStatsManager.EffectWithGivenDelta(new PlayerStats
                {
                    MaxMovementSpeed = destroyed.SpeedUpFactor
                });
            }
        }

        public void HandleAsteroidCollision(Asteroid hitBy)
        {
            if (this.m_slowDownIfHit)
            {
                Debug.Log($"Slow down player by {hitBy.SlowDownFactor}");
                this.m_playerStatsManager.EffectWithGivenDelta(new PlayerStats
                {
                    MaxMovementSpeed = -hitBy.SlowDownFactor
                });
            }

            this.m_shieldController.InflictDamage(hitBy.Damage);
        }

        private IEnumerator ActivateAsteroidSlowDownEffect()
        {
            yield return new WaitForSeconds(this.m_activateAsteroidSlowDownAfterSeconds);
            this.m_slowDownIfHit = true;
        }

        private IEnumerator ActivateIncreaseEffect()
        {
            yield return new WaitForSeconds(this.m_increasePlayerSpeedAfterSeconds);
            this.m_slowDownIfHit = true;
        }

        private IEnumerator ActivateShieldSlowDownEffect()
        {
            yield return new WaitForSeconds(this.m_activateShieldSlowDownAfterSeconds);
            this.m_slowDownIfHit = true;
        }

        private void OnShieldValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            if (e.NewValue < 1 && this.m_slowDownIfLostShield)
            {
                Debug.Log("Shield lost, losing 1 speed.");
                this.m_playerStatsManager.EffectWithGivenDelta(new PlayerStats
                {
                    MaxMovementSpeed = -1
                });
            }
        }
    }
}