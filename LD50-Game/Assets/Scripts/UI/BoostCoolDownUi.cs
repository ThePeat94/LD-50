using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class BoostCoolDownUi : MonoBehaviour
    {
        [SerializeField] private Slider m_cooldownSlider;

        private BoostController m_boostController;

        private void Awake()
        {
            this.m_boostController = FindObjectOfType<BoostController>();
        }

        private void Start()
        {
            this.m_cooldownSlider.maxValue = this.m_boostController.BoostCoolDown;
        }

        private void Update()
        {
            this.m_cooldownSlider.value = this.m_boostController.CurrentBoostCoolDown;
        }
    }
}