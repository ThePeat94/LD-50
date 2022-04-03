using UnityEngine;

namespace Nidavellir
{
    public class SpawnableObject : MonoBehaviour
    {
        [SerializeField] private Transform m_modelTransform;

        private Rigidbody m_rigidbody;
        private Vector3 m_rotations;
        private Vector3 m_velocity;

        private void Awake()
        {
            this.m_rigidbody = this.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            this.m_modelTransform.RotateAround(this.m_modelTransform.position, this.m_modelTransform.right, this.m_rotations.x * Time.deltaTime);
            this.m_modelTransform.RotateAround(this.m_modelTransform.position, this.m_modelTransform.up, this.m_rotations.y * Time.deltaTime);
            this.m_modelTransform.RotateAround(this.m_modelTransform.position, this.m_modelTransform.right, this.m_rotations.z * Time.deltaTime);
            this.m_rigidbody.velocity = this.m_velocity;
        }

        public void SetConstantVelocity(Vector3 velocity)
        {
            this.m_velocity = velocity;
        }

        public void SetRandomRotation(Vector3 rotationsPerAxis)
        {
            this.m_rotations = rotationsPerAxis;
        }
    }
}