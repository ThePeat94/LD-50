using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Music Data", menuName = "Music Data", order = 0)]
    public class MusicData : ScriptableObject
    {
        [SerializeField] private AudioClip m_musicClip;
        [SerializeField] private float m_volume = 1f;
        [SerializeField] private bool m_looping;
        [SerializeField] private MusicData m_followingClip;

        public AudioClip MusicClip => this.m_musicClip;
        public float Volume => this.m_volume;
        public bool Looping => this.m_looping;
        public MusicData FollowingClip => this.m_followingClip;
    }
}