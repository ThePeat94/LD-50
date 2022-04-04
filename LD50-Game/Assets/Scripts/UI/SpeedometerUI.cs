using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class SpeedometerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_speedometer;

        private void Update()
        {
            var text = PlayerController.Instance.Velocity.z >= 0 ? "" : "-";
            text += $"{PlayerController.Instance.Speed:F0} ls/h";
            this.m_speedometer.text = text;
        }
    }
}