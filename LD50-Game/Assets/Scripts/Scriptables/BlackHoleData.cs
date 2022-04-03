using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Black Hole Data", menuName = "Black Hole Data", order = 0)]
    public class BlackHoleData : ScriptableObject
    {
        [SerializeField] private float m_speed;
        [SerializeField] private float m_slowDangerDistance;
        [SerializeField] private float m_fastDangerDistance;
        [SerializeField] private float m_slingDistance;
        [SerializeField] [Range(0f, 1f)] private float m_slingStrength;
        [SerializeField] private float m_minSpeed;

        public float MinSpeed => this.m_minSpeed;
        public float Speed => this.m_speed;
        public float SlowDangerDistance => this.m_slowDangerDistance;
        public float FastDangerDistance => this.m_fastDangerDistance;
        public float SlingDistance => this.m_slingDistance;
        public float SlingStrength => this.m_slingStrength;
    }
}