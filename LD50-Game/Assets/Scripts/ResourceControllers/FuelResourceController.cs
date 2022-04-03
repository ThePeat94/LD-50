using Scriptables;
using UnityEngine;

namespace Nidavellir.ResourceControllers
{
    public class FuelResourceController : MonoBehaviour
    {
        [SerializeField] private ResourceData m_fuelData;
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private float m_maximumFuelUsagePerSecond;


        private PlayerController m_playerController;

        public ResourceController ResourceController { get; private set; }
        public bool CanNavigate => this.ResourceController.CurrentValue > 0.1f;

        private void Awake()
        {
            this.ResourceController = new ResourceController(this.m_fuelData);
            this.m_playerController = this.GetComponent<PlayerController>();
        }

        public void Update()
        {
            if (!this.CanNavigate)
                return;

            var currentPercentage = this.m_playerController.Velocity / this.m_playerData.MovementSpeed;
            var currentFuelUsage = this.m_maximumFuelUsagePerSecond * currentPercentage;
            this.ResourceController.UseResource(currentFuelUsage * Time.deltaTime);
        }
    }
}