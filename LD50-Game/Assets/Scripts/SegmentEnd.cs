using UnityEngine;

namespace Nidavellir
{
    public class SegmentEnd : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out _))
                FindObjectOfType<ObjectPool>()
                    .PutLastToFirstPosition();
        }
    }
}