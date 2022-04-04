using System.Collections;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class BoostController : MonoBehaviour
    {
        [SerializeField] private BoostData m_boostData;
        [SerializeField] private SfxData m_chargedSfx;

        private Coroutine m_boostCoroutine;
        private Coroutine m_chargeCoroutine;

        private int m_currentBoostFrameCount;

        private InputProcessor m_inputProcessor;
        private OneShotSfxPlayer m_oneShotSfxPlayer;
        private PlayerStatsManager m_playerStatsManager;
        private RandomClipPlayer m_randomClipPlayer;

        public int CurrentBoostCoolDown { get; private set; }
        public int BoostCoolDown => this.m_boostData.FrameCountCooldown;

        public BoostData BoostData => this.m_boostData;


        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
            this.CurrentBoostCoolDown = this.m_boostData.FrameCountCooldown;
            this.m_randomClipPlayer = this.GetComponent<RandomClipPlayer>();
            this.m_oneShotSfxPlayer = this.GetComponent<OneShotSfxPlayer>();
        }

        private void Update()
        {
            if (this.m_inputProcessor.IsBoosting && this.m_boostCoroutine == null && this.CurrentBoostCoolDown >= this.m_boostData.FrameCountCooldown)
            {
                this.m_boostCoroutine = this.StartCoroutine(this.Boost());
            }
        }

        private void FixedUpdate()
        {
            if (this.m_boostCoroutine != null)
                this.m_currentBoostFrameCount++;
            else if (this.m_chargeCoroutine == null && this.CurrentBoostCoolDown < this.m_boostData.FrameCountCooldown)
                this.m_chargeCoroutine = this.StartCoroutine(this.RechargeBoost());
        }

        private IEnumerator Boost()
        {
            var delta = new PlayerStats
            {
                MaxMovementSpeed = this.m_boostData.MovementSpeedBoost,
                Acceleration = this.m_boostData.AccelerationBoost
            };

            this.m_playerStatsManager.EffectWithGivenDelta(delta);
            this.m_randomClipPlayer.PlayRandomOneShot();


            while (this.m_inputProcessor.IsBoosting && this.m_currentBoostFrameCount <= this.m_boostData.BoostFrameCountDuration)
                yield return new WaitForEndOfFrame();

            this.CurrentBoostCoolDown = 0;
            this.m_playerStatsManager.RemoveEffect(delta);
            this.m_boostCoroutine = null;
            this.m_currentBoostFrameCount = 0;
        }

        private IEnumerator RechargeBoost()
        {
            while (this.CurrentBoostCoolDown < this.m_boostData.FrameCountCooldown)
            {
                yield return new WaitForFixedUpdate();
                this.CurrentBoostCoolDown++;
            }

            this.m_oneShotSfxPlayer.PlayOneShot(this.m_chargedSfx);
            this.m_boostCoroutine = null;
        }
    }
}