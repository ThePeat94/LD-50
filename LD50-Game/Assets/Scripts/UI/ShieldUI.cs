using EventArgs;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class ShieldUI : MonoBehaviour
    {
        [SerializeField] private ShieldController m_shieldController;
        [SerializeField] private Slider m_slider;

        private void Start()
        {
            this.m_slider.value = this.m_shieldController.ResourceController.CurrentValue;
            this.m_slider.maxValue = this.m_shieldController.ResourceController.MaxValue;
            this.m_shieldController.ResourceController.ResourceValueChanged += this.ShieldValueChanged;
        }

        private void ShieldValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_slider.value = e.NewValue;
        }
    }
}