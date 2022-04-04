using UnityEngine;

namespace Nidavellir
{
    public class Projectile : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(this.gameObject, 2f);
        }

        private void OnTriggerEnter(Collider other)
        {
            var hittable = other.GetComponentInParent<IHittable>();
            if (hittable != null)
            {
                hittable.Hit();
                Destroy(this.gameObject);
            }
        }
    }
}