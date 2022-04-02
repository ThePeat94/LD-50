using UnityEngine;

namespace Nidavellir
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        private Rigidbody m_rigidbody;

        private void Start()
        {
            this.m_rigidbody = this.GetComponent<Rigidbody>();
        }

        public void Update()
        {
            var velocityDifference = this.m_speed;
            this.m_rigidbody.velocity = Vector3.forward * velocityDifference;
        }
    }
}