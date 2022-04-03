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
        [SerializeField] private AudioClip m_gameTheme;
        private AudioSource m_audioSource;


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
            if (hasLoadedMainMenu)
                this.PlayLoopingMusic(this.m_titleTheme);
            else if (loadedScene.buildIndex == 1)
                this.PlayLoopingMusic(this.m_gameTheme);
        }
    }
}