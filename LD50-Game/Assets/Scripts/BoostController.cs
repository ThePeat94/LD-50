using System.Collections;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class BoostController : MonoBehaviour
    {
        [SerializeField] private BoostData m_boostData;
        private Coroutine m_boostCoroutine;

        private int m_currentBoostFrameCount;

        private InputProcessor m_inputProcessor;
        private PlayerStatsManager m_playerStatsManager;

        public int CurrentBoostCoolDown { get; private set; }

        public BoostData BoostData => this.m_boostData;


        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_playerStatsManager = this.GetComponent<PlayerStatsManager>();
            this.CurrentBoostCoolDown = this.m_boostData.FrameCountCooldown;
        }

        private void Update()
        {
            if (this.m_inputProcessor.IsBoosting && this.m_boostCoroutine == null && this.CurrentBoostCoolDown > this.m_boostData.FrameCountCooldown)
            {
                this.m_boostCoroutine = this.StartCoroutine(this.Boost());
                this.CurrentBoostCoolDown = 0;
            }
        }

        private void FixedUpdate()
        {
            if (this.m_boostCoroutine != null)
                this.m_currentBoostFrameCount++;
            else
                this.CurrentBoostCoolDown++;
        }

        private IEnumerator Boost()
        {
            var delta = new PlayerStats
            {
                MaxMovementSpeed = this.m_boostData.MovementSpeedBoost,
                Acceleration = this.m_boostData.AccelerationBoost
            };

            this.m_playerStatsManager.EffectWithGivenDelta(delta);


            while (this.m_inputProcessor.IsBoosting && this.m_currentBoostFrameCount <= this.m_boostData.BoostFrameCountDuration)
                yield return new WaitForEndOfFrame();

            this.m_playerStatsManager.RemoveEffect(delta);
            this.m_boostCoroutine = null;
            this.m_currentBoostFrameCount = 0;
        }
    }
}