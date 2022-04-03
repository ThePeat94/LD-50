using EventArgs;
using Nidavellir.ResourceControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FuelUI : MonoBehaviour
    {
        [SerializeField] private Slider m_slider;
        [SerializeField] private FuelResourceController m_fuelResourceController;


        private void Start()
        {
            this.m_slider.maxValue = this.m_fuelResourceController.ResourceController.MaxValue;
            this.m_slider.value = this.m_fuelResourceController.ResourceController.CurrentValue;

            this.m_fuelResourceController.ResourceController.MaxValueChanged += this.OnMaxValueChanged;
            this.m_fuelResourceController.ResourceController.ResourceValueChanged += this.OnValueChanged;
        }

        private void OnMaxValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_slider.maxValue = e.NewValue;
        }

        private void OnValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_slider.value = e.NewValue;
        }
    }
}