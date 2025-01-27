﻿using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Fuel Controller Data", menuName = "Fuel Controller Data", order = 0)]
    public class FuelControllerData : ScriptableObject
    {
        [SerializeField] private int m_speedUpgradeAfter;
        [SerializeField] private float m_speedUpgradeAmount;
        [SerializeField] private int m_tankUpgradeAfter;
        [SerializeField] private float m_tankUpgradeAmount;

        public int SpeedUpgradeAfter => this.m_speedUpgradeAfter;
        public float SpeedUpgradeAmount => this.m_speedUpgradeAmount;
        public int TankUpgradeAfter => this.m_tankUpgradeAfter;
        public float TankUpgradeAmount => this.m_tankUpgradeAmount;
    }
}