using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Progress Data", menuName = "Progress Data", order = 0)]
    public class ProgressData : ScriptableObject
    {
        [SerializeField] private BlackHoleProgressData m_blackHoleData;
        [SerializeField] private List<ObjectSpawnerData> m_spawnerData;
        [SerializeField] private float m_activatesAfterSeconds;

        public BlackHoleProgressData BlackHoleData => this.m_blackHoleData;
        public List<ObjectSpawnerData> SpawnerData => this.m_spawnerData;
        public float ActivatesAfterSeconds => this.m_activatesAfterSeconds;
    }
}