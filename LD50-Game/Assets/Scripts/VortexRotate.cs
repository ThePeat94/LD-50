using UnityEngine;

namespace Nidavellir
{
    public class VortexRotate : MonoBehaviour
    {
        [SerializeField] private float m_rotationSpeed;

        private void Update()
        {
            this.transform.RotateAround(this.transform.position, Vector3.up, this.m_rotationSpeed * Time.deltaTime);
        }
    }
}