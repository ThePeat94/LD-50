using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class SpeedometerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_speedometer;

        private void Update()
        {
            var text = string.Empty;
            if (Mathf.Abs(PlayerController.Instance.Velocity.normalized.z) < 0.35f)
            {
                text = "0 ls/s";
            }
            else
            {
                text = PlayerController.Instance.Velocity.z >= 0 ? "" : "-";
                text += $"{PlayerController.Instance.Speed:F0} ls/h";
            }

            this.m_speedometer.text = text;
        }
    }
}