using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Arsenal Data", menuName = "Arsenal Data", order = 0)]
    public class ArsenalData : ScriptableObject
    {
        [SerializeField] private float m_shootFrequency;
        [SerializeField] private float m_shootCooldown;

        public float ShootFrequency => this.m_shootFrequency;
        public float ShootCooldown => this.m_shootCooldown;
    }
}