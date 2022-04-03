using Nidavellir.ResourceControllers;
using UnityEngine;

namespace Nidavellir
{
    public class FuelCanister : MonoBehaviour
    {
        [SerializeField] private float m_amount;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<FuelResourceController>(out var fuelController))
            {
                fuelController.ResourceController.Add(this.m_amount);
                Destroy(this.gameObject);
            }
        }
    }
}