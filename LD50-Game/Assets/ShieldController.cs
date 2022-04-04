using System.Collections;
using EventArgs;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private ResourceData m_resourceData;

        private ShieldState m_currentState = ShieldState.Ready;
        private PlayerStatsManager m_playerStatsManager;
        private Coroutine m_rechargeCoroutine;

        public ResourceController ResourceController { get; private set; }

        private void Awake()
        {
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
            this.ResourceController = new ResourceController(this.m_resourceData);
            this.ResourceController.ResourceValueChanged += this.OnShieldValueChanged;
        }

        public void AddCharge()
        {
            if (this.m_currentState != ShieldState.Ready)
                return;

            this.ResourceController.Add(1);
        }

        public void InflictDamage(int amount)
        {
            if (this.m_currentState != ShieldState.Ready)
                return;

            var toInflict = amount > this.ResourceController.CurrentValue ? this.ResourceController.CurrentValue : amount;
            this.ResourceController.UseResource(toInflict);
        }

        private void OnShieldValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            if (e.NewValue == 0 && this.m_currentState != ShieldState.Recharging)
            {
                var delta = new PlayerStats
                {
                    MaxMovementSpeed = -10f,
                    Acceleration = -25f
                };
                this.m_playerStatsManager.EffectWithGivenDelta(delta);

                this.m_rechargeCoroutine = this.StartCoroutine(this.RechargeShield(delta));
            }
        }

        private IEnumerator RechargeShield(PlayerStats delta)
        {
            this.m_currentState = ShieldState.Recharging;
            yield return new WaitForSeconds(3f);
            for (var i = 0; i < this.ResourceController.MaxValue; i++)
            {
                this.ResourceController.Add(1);
                yield return new WaitForSeconds(0.5f);
            }

            this.m_playerStatsManager.RemoveEffect(delta);
            this.m_currentState = ShieldState.Ready;
            this.m_rechargeCoroutine = null;
        }

        private enum ShieldState
        {
            Ready,
            Recharging
        }
    }
}