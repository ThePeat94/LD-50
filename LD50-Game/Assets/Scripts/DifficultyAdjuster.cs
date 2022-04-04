using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class DifficultyAdjuster : MonoBehaviour
    {
        [SerializeField] private List<ProgressData> m_progressData;

        private BlackHole m_blackHole;
        private ObjectSpawner m_objectSpawner;

        private void Awake()
        {
            this.m_blackHole = FindObjectOfType<BlackHole>();
            this.m_objectSpawner = FindObjectOfType<ObjectSpawner>();
        }


        private void Start()
        {
            foreach (var progressData in this.m_progressData)
                this.StartCoroutine(this.ActivateData(progressData));
        }

        private IEnumerator ActivateData(ProgressData progressData)
        {
            yield return new WaitForSeconds(progressData.ActivatesAfterSeconds);
            if (progressData.BlackHoleData)
                this.m_blackHole.ActivateData(progressData.BlackHoleData);
            this.m_objectSpawner.ActivateData(progressData.SpawnerData);
        }
    }
}