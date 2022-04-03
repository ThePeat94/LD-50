using System.Collections;
using Scriptables;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir
{
    public class OneShotSfxPlayer : MonoBehaviour
    {
        public void PlayOneShot(SfxData sfxData)
        {
            this.StartCoroutine(this.PlayClipAndDestroySource(sfxData.AudioClip, sfxData.Volume));
        }

        private IEnumerator PlayClipAndDestroySource(AudioClip clip, float volume)
        {
            var audioSource = this.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume * GlobalSettings.Instance.SfxVolume;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length);
            Destroy(audioSource);
        }
    }
}