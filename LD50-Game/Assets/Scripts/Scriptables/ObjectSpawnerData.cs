using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Object Spawner Data", menuName = "Object Spawner Data", order = 0)]
    public class ObjectSpawnerData : ScriptableObject
    {
        [SerializeField] private float m_minVelocity;
        [SerializeField] private float m_maxVelocity;
        [SerializeField] private float m_minRotationSpeed;
        [SerializeField] private float m_maxRotationSpeed;
        [SerializeField] private float m_frameCoolDown;
        [SerializeField] private GameObject m_toSpawn;

        public float MinVelocity => this.m_minVelocity;
        public float MaxVelocity => this.m_maxVelocity;
        public float MinRotationSpeed => this.m_minRotationSpeed;
        public float MaxRotationSpeed => this.m_maxRotationSpeed;
        public float FrameCoolDown => this.m_frameCoolDown;
        public GameObject ToSpawn => this.m_toSpawn;
    }
}