using UnityEngine;

namespace Nidavellir
{
    public class Projectile : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(this.gameObject, 5f);
        }
    }
}