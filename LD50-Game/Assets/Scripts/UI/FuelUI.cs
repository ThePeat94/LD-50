using System.Collections;
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
        [SerializeField] private Image m_sliderFillImage;
        [SerializeField] private Color m_blinkColor;
        private Coroutine m_blinkCoroutine;


        private Color m_originalColor;
        private bool m_shouldBlink;

        private void Awake()
        {
            this.m_originalColor = this.m_sliderFillImage.color;
        }

        private void Start()
        {
            this.m_slider.maxValue = this.m_fuelResourceController.ResourceController.MaxValue;
            this.m_slider.value = this.m_fuelResourceController.ResourceController.CurrentValue;

            this.m_fuelResourceController.ResourceController.MaxValueChanged += this.OnMaxValueChanged;
            this.m_fuelResourceController.ResourceController.ResourceValueChanged += this.OnValueChanged;
        }

        private IEnumerator BlinkLowFuel()
        {
            while (this.m_shouldBlink)
            {
                this.m_sliderFillImage.color = Color.Lerp(this.m_originalColor, this.m_blinkColor, Mathf.Sin(Time.time * 10) * 0.5f + 0.5f);
                yield return new WaitForEndOfFrame();
            }

            this.m_sliderFillImage.color = this.m_originalColor;
            this.m_blinkCoroutine = null;
        }

        private void OnMaxValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_slider.maxValue = e.NewValue;
        }

        private void OnValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_slider.value = e.NewValue;

            if (this.m_fuelResourceController.ResourceController.CurrentValue / this.m_fuelResourceController.ResourceController.MaxValue <= 0.33f)
            {
                this.m_shouldBlink = true;
                if (this.m_blinkCoroutine == null)
                    this.m_blinkCoroutine = this.StartCoroutine(this.BlinkLowFuel());
            }
            else
            {
                this.m_shouldBlink = false;
            }
        }
    }
}