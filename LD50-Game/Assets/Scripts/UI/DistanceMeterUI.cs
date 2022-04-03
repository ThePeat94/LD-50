using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class DistanceMeterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;

        private void Update()
        {
            this.m_text.text = $"{PlayerController.Instance.PassedUnits:F2} Lightyears";
        }
    }
}