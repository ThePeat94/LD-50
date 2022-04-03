using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Boost Data", menuName = "Boost Data", order = 0)]
    public class BoostData : ScriptableObject
    {
        [SerializeField] private float m_movementSpeedBoost;
        [SerializeField] private float m_accelerationBoost;
        [SerializeField] private int m_frameCountCooldown;
        [SerializeField] private int m_boostFrameCountDuration;

        public float MovementSpeedBoost => this.m_movementSpeedBoost;
        public float AccelerationBoost => this.m_accelerationBoost;
        public int FrameCountCooldown => this.m_frameCountCooldown;
        public int BoostFrameCountDuration => this.m_boostFrameCountDuration;
    }
}