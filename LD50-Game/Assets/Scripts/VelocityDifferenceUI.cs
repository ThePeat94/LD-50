using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir
{
    public class VelocityDifferenceUI : MonoBehaviour
    {
        [SerializeField] private Slider m_playerAdvantageSlider;
        [SerializeField] private Slider m_blackHoleAdvantageSlider;

        private BlackHole m_blackHole;

        private void Awake()
        {
            this.m_blackHole = FindObjectOfType<BlackHole>();
            this.m_playerAdvantageSlider.value = 0;
            this.m_blackHoleAdvantageSlider.value = 0;
        }

        private void Update()
        {
            var difference = PlayerController.Instance.Speed - this.m_blackHole.Speed;

            if (difference >= 0)
            {
                this.m_playerAdvantageSlider.value = difference;
                this.m_blackHoleAdvantageSlider.value = 0;
            }
            else
            {
                this.m_playerAdvantageSlider.value = 0;
                this.m_blackHoleAdvantageSlider.value = Mathf.Abs(difference);
            }
        }
    }
}