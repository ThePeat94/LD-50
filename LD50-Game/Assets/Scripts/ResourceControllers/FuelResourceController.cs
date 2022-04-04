using System.Collections;
using EventArgs;
using Scriptables;
using UnityEngine;

namespace Nidavellir.ResourceControllers
{
    public class FuelResourceController : MonoBehaviour
    {
        [SerializeField] private ResourceData m_fuelData;
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private float m_maximumFuelUsagePerSecond;
        [SerializeField] private SfxData m_fuelLowSfx;
        [SerializeField] private FuelControllerData m_fuelControllerData;

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

            var currentPercentage = this.m_playerController.Speed / this.m_playerData.MovementSpeed;
            var currentFuelUsage = this.m_maximumFuelUsagePerSecond * currentPercentage;
            this.ResourceController.UseResource(currentFuelUsage * Time.deltaTime);
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
                if (this.m_playSfxCoroutine == null)
                    this.m_playSfxCoroutine = this.StartCoroutine(this.PlaySfx());
        }

        private IEnumerator PlaySfx()
        {
            this.m_oneShotSfxPlayer.PlayOneShot(this.m_fuelLowSfx);
            yield return new WaitForSeconds(this.m_fuelLowSfx.AudioClip.length);
            this.m_playSfxCoroutine = null;
        }
    }
}