using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "SfxData", menuName = "Sfx Data", order = 0)]
    public class SfxData : ScriptableObject
    {
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private float m_volume;

        public AudioClip AudioClip => this.m_audioClip;
        public float Volume => this.m_volume;
    }
}