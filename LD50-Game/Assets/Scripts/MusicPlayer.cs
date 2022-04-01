using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip m_titleTheme;
        [SerializeField] private AudioClip m_gameThemeIntro;
        [SerializeField] private AudioClip m_gameTheme;
        private AudioSource m_audioSource;

        private List<AudioClip> m_gameThemeClips;

        private int m_lastLoadedSceneIndex;
        private Coroutine m_queueRoutine;

        public static MusicPlayer Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                SceneManager.sceneLoaded += this.SceneChanged;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            this.m_audioSource = this.GetComponent<AudioSource>();
            this.m_gameThemeClips = new List<AudioClip>
            {
                this.m_gameThemeIntro,
                this.m_gameTheme
            };
        }

        public void PlayClips(List<AudioClip> clipQueue)
        {
            if (this.m_queueRoutine != null)
            {
                this.StopCoroutine(this.m_queueRoutine);
                this.m_queueRoutine = null;
                this.m_audioSource.Stop();
            }

            this.m_audioSource.loop = true;
            this.m_queueRoutine = this.StartCoroutine(this.PlayQueue(clipQueue));
        }

        public void PlayLoopingMusic(AudioClip toPlay)
        {
            if (this.m_queueRoutine != null)
            {
                this.StopCoroutine(this.m_queueRoutine);
                this.m_queueRoutine = null;
            }

            this.PlayClip(toPlay, true);
        }

        public void PlayMusicOnce(AudioClip toPlay)
        {
            if (this.m_queueRoutine != null)
            {
                this.StopCoroutine(this.m_queueRoutine);
                this.m_queueRoutine = null;
            }

            this.PlayClip(toPlay, false);
        }

        private void PlayClip(AudioClip toPlay, bool loop)
        {
            this.m_audioSource.clip = toPlay;
            this.m_audioSource.loop = loop;
            this.m_audioSource.Play();
        }

        private IEnumerator PlayQueue(List<AudioClip> toPlay)
        {
            foreach (var current in toPlay)
            {
                this.m_audioSource.clip = current;
                this.m_audioSource.Play();
                yield return new WaitForSeconds(current.length);
            }
        }

        private void SceneChanged(Scene loadedScene, LoadSceneMode arg1)
        {
            var hasLoadedMainMenu = loadedScene.buildIndex == 0;
            var hasSceneChanged = this.m_lastLoadedSceneIndex != loadedScene.buildIndex;
            if (hasLoadedMainMenu)
                this.PlayLoopingMusic(this.m_titleTheme);
            else if (hasSceneChanged)
                this.PlayClips(this.m_gameThemeClips);

            this.m_lastLoadedSceneIndex = loadedScene.buildIndex;
        }
    }
}