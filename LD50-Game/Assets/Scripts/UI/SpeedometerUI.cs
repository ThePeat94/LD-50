using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class SpeedometerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_speedometer;

        private void Update()
        {
            this.m_speedometer.text = $"{PlayerController.Instance.Velocity:F2} ly/h";
        }
    }
}