using UnityEngine;
using UnityEngine.Audio;

namespace Nidavellir
{
    public class RandomClipPlayer : MonoBehaviour
    {
        [SerializeField] private bool m_concurrentPlyback;
        [SerializeField] private AudioClip[] m_audioClips;
        [SerializeField] private AudioMixerGroup m_audioMixerGroup;

        private AudioSource[] m_audioSources;

        private void Awake()
        {
            // TODO: pool the AudioSources. When this.m_concurrentPlyback == true then only different clips will play concurrently
            // but if we play the same clip twice, the second one will stop the first one (because each clip has a dedicated audiosource)
            // instead, use a pool of n AudioSources for m clips and then just pool them, so we can have any n clips play at once.

            var audioSourcesCount = this.m_concurrentPlyback ? this.m_audioClips.Length : 1;
            this.m_audioSources = new AudioSource[audioSourcesCount];

            for (var i = 0; i < audioSourcesCount; i++)
            {
                var audioSource = this.gameObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = this.m_audioMixerGroup;
                audioSource.playOnAwake = false;
                this.m_audioSources[i] = audioSource;
            }
        }

        public void PlayRandomOneShot()
        {
            var audioClipIndex = Random.Range(0, this.m_audioClips.Length);
            var audioSourceIndex = this.m_concurrentPlyback ? audioClipIndex : 0;
            this.m_audioSources[audioSourceIndex]
                .PlayOneShot(this.m_audioClips[audioClipIndex]);
        }
    }
}