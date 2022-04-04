using System.Collections;
using EventArgs;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class ShieldUI : MonoBehaviour
    {
        [SerializeField] private ShieldController m_shieldController;
        [SerializeField] private Slider m_slider;
        [SerializeField] private Image m_sliderFillImage;
        [SerializeField] private Color m_blinkColor;

        private Coroutine m_blinkCoroutine;
        private Color m_originalColor;

        private void Start()
        {
            this.m_slider.maxValue = this.m_shieldController.ResourceController.MaxValue;
            this.m_slider.value = this.m_shieldController.ResourceController.CurrentValue;
            this.m_shieldController.ResourceController.ResourceValueChanged += this.ShieldValueChanged;
            this.m_shieldController.ResourceController.MaxValueChanged += this.OnMaxShieldValueChanged;
            this.m_shieldController.StartedRecharging += this.OnStartedRecharging;
            this.m_shieldController.FinishedRecharging += this.OnShieldFinishedCharging;
            this.m_originalColor = this.m_sliderFillImage.color;
        }

        private void OnMaxShieldValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_slider.maxValue = e.NewValue;
        }

        private void OnShieldFinishedCharging(object sender, System.EventArgs e)
        {
            if (this.m_blinkCoroutine != null)
            {
                this.StopCoroutine(this.m_blinkCoroutine);
                this.m_blinkCoroutine = null;
            }

            this.m_slider.maxValue = this.m_shieldController.ResourceController.MaxValue;
            this.m_slider.value = this.m_shieldController.ResourceController.CurrentValue;
            this.m_sliderFillImage.color = this.m_originalColor;
        }

        private void OnStartedRecharging(object sender, System.EventArgs e)
        {
            if (this.m_blinkCoroutine == null)
                this.m_blinkCoroutine = this.StartCoroutine(this.StartBlinking());
        }

        private void ShieldValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.m_slider.value = e.NewValue;
        }

        private IEnumerator StartBlinking()
        {
            this.m_slider.maxValue = this.m_shieldController.RechargeFrameCount;
            this.m_slider.value = 0;

            while (this.m_shieldController.CurrentState == ShieldState.Recharging)
            {
                yield return new WaitForEndOfFrame();
                this.m_sliderFillImage.color = Color.Lerp(this.m_originalColor, this.m_blinkColor, Mathf.Sin(Time.time * 5) * 0.5f + 0.5f);
                this.m_slider.value = this.m_shieldController.CurrentRechargeFrameCount;
            }

            this.m_blinkCoroutine = null;
        }
    }
}