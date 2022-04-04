using System;
using System.Collections;
using EventArgs;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private ResourceData m_resourceData;
        [SerializeField] private ShieldControllerData m_shieldControllerData;
        [SerializeField] private SfxData m_shieldDownSfx;


        private int m_collectedShields;

        private EventHandler m_finishedRecharging;
        private OneShotSfxPlayer m_oneShotSfxPlayer;

        private PlayerStatsManager m_playerStatsManager;
        private Coroutine m_rechargeCoroutine;
        private EventHandler m_startedRecharging;

        public ResourceController ResourceController { get; private set; }
        public int CurrentRechargeFrameCount { get; private set; }

        public int RechargeFrameCount => this.m_shieldControllerData.RechargeCooldownFrameCount;
        public ShieldState CurrentState { get; private set; } = ShieldState.Ready;

        public event EventHandler FinishedRecharging
        {
            add => this.m_finishedRecharging += value;
            remove => this.m_finishedRecharging -= value;
        }

        public event EventHandler StartedRecharging
        {
            add => this.m_startedRecharging += value;
            remove => this.m_startedRecharging -= value;
        }

        private void Awake()
        {
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
            this.ResourceController = new ResourceController(this.m_resourceData);
            this.ResourceController.ResourceValueChanged += this.OnShieldValueChanged;
            this.m_oneShotSfxPlayer = this.GetComponent<OneShotSfxPlayer>();
        }

        public void AddCharge()
        {
            this.m_collectedShields++;
            if (this.CurrentState == ShieldState.Ready)
            {
                this.ResourceController.Add(1);

                if (this.m_collectedShields % this.m_shieldControllerData.IncreaseMaxShieldAfterAmount == 0)
                    this.ResourceController.IncreaseMaximum(1);
            }
            else if (this.CurrentState == ShieldState.Recharging)
            {
                this.CurrentRechargeFrameCount += this.m_shieldControllerData.ShieldRechargeFrameAmount;
            }
        }

        public void InflictDamage(int amount)
        {
            if (this.CurrentState != ShieldState.Ready)
                return;

            var toInflict = amount > this.ResourceController.CurrentValue ? this.ResourceController.CurrentValue : amount;
            this.ResourceController.UseResource(toInflict);
        }

        private void OnShieldValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            if (e.NewValue == 0 && this.CurrentState != ShieldState.Recharging)
            {
                var delta = new PlayerStats
                {
                    MaxMovementSpeed = -10f,
                    Acceleration = -25f
                };
                this.m_playerStatsManager.EffectWithGivenDelta(delta);

                if (this.m_rechargeCoroutine == null)
                    this.m_rechargeCoroutine = this.StartCoroutine(this.RechargeShield(delta));

                this.m_oneShotSfxPlayer.PlayOneShot(this.m_shieldDownSfx);
            }
        }

        private IEnumerator RechargeShield(PlayerStats delta)
        {
            this.CurrentRechargeFrameCount = 0;
            this.CurrentState = ShieldState.Recharging;
            this.m_startedRecharging?.Invoke(this, System.EventArgs.Empty);

            while (this.CurrentRechargeFrameCount < this.m_shieldControllerData.RechargeCooldownFrameCount)
            {
                yield return new WaitForFixedUpdate();
                this.CurrentRechargeFrameCount++;
            }

            this.m_finishedRecharging?.Invoke(this, System.EventArgs.Empty);

            for (var i = 0; i < this.ResourceController.MaxValue; i++)
            {
                this.ResourceController.Add(1);
                yield return new WaitForSeconds(this.m_shieldControllerData.RechargeIntervalSeconds);
            }

            this.CurrentRechargeFrameCount = 0;
            this.m_playerStatsManager.RemoveEffect(delta);
            this.CurrentState = ShieldState.Ready;
            this.m_rechargeCoroutine = null;
        }
    }
}