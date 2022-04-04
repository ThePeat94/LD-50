using UnityEngine;

namespace Nidavellir.UI
{
    public class RotateUiElement : MonoBehaviour
    {
        [SerializeField] private float m_rotationSpeed;

        private void Update()
        {
            this.transform.RotateAround(this.transform.position, Vector3.up, this.m_rotationSpeed * Time.deltaTime);
        }
    }
}