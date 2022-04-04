using UnityEngine;

namespace Nidavellir.UI
{
    public class RotateUiElement : MonoBehaviour
    {
        [SerializeField] private float m_rotationSpeed;

        private void Update()
        {
            this.transform.RotateAround(this.transform.position, this.transform.forward, this.m_rotationSpeed * Time.deltaTime);
        }
    }
}