using UnityEngine;

namespace Nidavellir
{
    public class Asteroid : MonoBehaviour, IHittable
    {
        [SerializeField] private int m_shieldDamage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ShieldController>(out var shieldController))
            {
                Debug.Log("Asteroid has hit player");
                shieldController.InflictDamage(this.m_shieldDamage);
                Destroy(this.gameObject);
            }
        }

        public void Hit()
        {
            Destroy(this.gameObject);
        }
    }
}