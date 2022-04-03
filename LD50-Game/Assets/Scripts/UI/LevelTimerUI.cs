using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class LevelTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_timerText;

        private void Update()
        {
            this.m_timerText.text = $"{LevelTimer.Instance.PastTimeSinceStart:mm\\:ss\\.ff}";
        }
    }
}