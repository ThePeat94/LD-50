using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir
{
    [RequireComponent(typeof(AudioListener))]
    public class OneShotSfxPlayer : MonoBehaviour
    {
        public void PlayOneShot(AudioClip clipToPlay, float volume = 1f)
        {
            if (clipToPlay == null)
                return;
            this.StartCoroutine(this.PlayClipAndDestroySource(clipToPlay, volume));
        }

        private IEnumerator PlayClipAndDestroySource(AudioClip clip, float volume)
        {
            var audioSource = this.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length);
            Destroy(audioSource);
        }
    }
}