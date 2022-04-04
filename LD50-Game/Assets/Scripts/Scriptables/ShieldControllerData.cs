using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Shield Controller Data", menuName = "Shield Controller Data", order = 0)]
    public class ShieldControllerData : ScriptableObject
    {
        [SerializeField] private int m_rechargeCooldownFrameCount;
        [SerializeField] private float m_rechargeIntervalSeconds;
        [SerializeField] private int m_shieldRechargeFrameAmount;
        [SerializeField] private int m_increaseMaxShieldAfterAmount;

        public int RechargeCooldownFrameCount => this.m_rechargeCooldownFrameCount;
        public float RechargeIntervalSeconds => this.m_rechargeIntervalSeconds;
        public int ShieldRechargeFrameAmount => this.m_shieldRechargeFrameAmount;
        public int IncreaseMaxShieldAfterAmount => this.m_increaseMaxShieldAfterAmount;
    }
}