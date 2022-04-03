using UnityEngine;

namespace Nidavellir
{
    public class Asteroid : MonoBehaviour, IHittable
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                Debug.Log("Asteroid has hit player");
                Destroy(this.gameObject);
            }
        }

        public void Hit()
        {
            Destroy(this.gameObject);
        }
    }
}