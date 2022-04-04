using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Fuel Controller Data", menuName = "Fuel Controller Data", order = 0)]
    public class FuelControllerData : ScriptableObject
    {
        [SerializeField] private int m_speedUpgradeAfter;
        [SerializeField] private int m_speedUpgradeAmount;
        [SerializeField] private int m_tankUpgradeAfter;
        [SerializeField] private int m_tankUpgradeAmount;

        public int SpeedUpgradeAfter => this.m_speedUpgradeAfter;
        public int SpeedUpgradeAmount => this.m_speedUpgradeAmount;
        public int TankUpgradeAfter => this.m_tankUpgradeAfter;
        public int TankUpgradeAmount => this.m_tankUpgradeAmount;
    }
}