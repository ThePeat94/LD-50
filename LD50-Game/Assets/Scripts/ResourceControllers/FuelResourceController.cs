﻿using EventArgs;
using Scriptables;
using UnityEngine;

namespace Nidavellir.ResourceControllers
{
    public class FuelResourceController : MonoBehaviour
    {
        [SerializeField] private ResourceData m_fuelData;
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private float m_maximumFuelUsagePerSecond;
        [SerializeField] private SfxData m_fuelEmptySfx;
        [SerializeField] private SfxData m_fuelLowWarningSfx;
        [SerializeField] private FuelControllerData m_fuelControllerData;
        [SerializeField] private AudioSource m_sfxAudioSource;

        private int m_collectedCanisterAmount;
        private OneShotSfxPlayer m_oneShotSfxPlayer;
        private PlayerController m_playerController;

        private PlayerStatsManager m_playerStatsManager;
        private Coroutine m_playSfxCoroutine;

        public ResourceController ResourceController { get; private set; }
        public bool CanNavigate => this.ResourceController.CurrentValue > 0.1f;

        private void Awake()
        {
            this.ResourceController = new ResourceController(this.m_fuelData);
            this.m_playerController = this.GetComponent<PlayerController>();
            this.ResourceController.ResourceValueChanged += this.OnFuelValueChanged;
            this.m_oneShotSfxPlayer = this.GetComponent<OneShotSfxPlayer>();
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
        }

        public void Update()
        {
            if (!this.CanNavigate)
                return;

            if (PlayerController.Instance.Velocity.magnitude <= 0.1f)
                return;

            var consumptionFactor = PlayerController.Instance.Acceleration > 0 ? 1f : 0.75f;

            var currentPercentage = this.m_playerController.Speed / this.m_playerData.MovementSpeed;
            var currentFuelUsage = this.m_maximumFuelUsagePerSecond * currentPercentage;
            this.ResourceController.UseResource(currentFuelUsage * Time.deltaTime * consumptionFactor);
        }

        public void AddCanister(float amount)
        {
            this.ResourceController.Add(amount);
            this.m_collectedCanisterAmount++;

            if (this.m_collectedCanisterAmount % this.m_fuelControllerData.SpeedUpgradeAfter == 0)
                this.m_playerStatsManager.EffectWithGivenDelta(new PlayerStats
                {
                    MaxMovementSpeed = this.m_fuelControllerData.SpeedUpgradeAmount
                });

            if (this.m_collectedCanisterAmount % this.m_fuelControllerData.TankUpgradeAfter == 0)
                this.ResourceController.IncreaseMaximum(this.m_fuelControllerData.TankUpgradeAmount);
        }

        private void OnFuelValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            if (e.NewValue <= 0.1f)
            {
                this.PlaySfx(this.m_fuelEmptySfx);
            }
            else if (e.NewValue / this.ResourceController.MaxValue <= 0.33f)
            {
                this.PlaySfx(this.m_fuelLowWarningSfx);
            }
            else
            {
                this.m_sfxAudioSource.Stop();
                this.m_sfxAudioSource.clip = null;
            }
        }

        private void PlaySfx(SfxData sfxData)
        {
            if (this.m_sfxAudioSource.clip != sfxData.AudioClip || this.m_sfxAudioSource.time >= sfxData.AudioClip.length)
            {
                this.m_sfxAudioSource.clip = sfxData.AudioClip;
                this.m_sfxAudioSource.volume = sfxData.Volume * GlobalSettings.Instance.SfxVolume;
                this.m_sfxAudioSource.Play();
            }
        }
    }
}