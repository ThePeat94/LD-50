using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Black Hole Progress Data", menuName = "Black Hole Progress Data", order = 0)]
    public class BlackHoleProgressData : ScriptableObject
    {
        [SerializeField] private float m_additionalSpeed;
        [SerializeField] private float m_extraSpeedPerDestroyedObject;

        public float AdditionalSpeed => this.m_additionalSpeed;
        public float ExtraSpeedPerDestroyedObject => this.m_extraSpeedPerDestroyedObject;
    }
}