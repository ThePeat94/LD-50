using UnityEngine;

namespace Nidavellir
{
    public class Projectile : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(this.gameObject, 5f);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Projectile hit {other.gameObject}");
            var hittable = other.GetComponentInParent<IHittable>();
            if (hittable != null)
            {
                hittable.Hit();
                Destroy(this.gameObject);
            }
        }
    }
}