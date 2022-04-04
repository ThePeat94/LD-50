using UnityEngine;

namespace Nidavellir
{
    public class SpaceshipMainMenu : MonoBehaviour
    {
        [SerializeField] private float m_startingRotationSpeed;
        [SerializeField] private float m_endRotatingSpeed;
        [SerializeField] private float m_startMovementSpeed;
        [SerializeField] private float m_endMovementSpeed;
        [SerializeField] private Rigidbody m_rigidbody;
        [SerializeField] private float m_reachMaxAfterSeconds;
        [SerializeField] private float m_xThreshold;
        private float m_currentMovementSpeed;

        private float m_currentRotationSpeed;
        private float m_currentTime;

        private Vector3 m_screenBounds;

        private void Awake()
        {
            this.m_currentMovementSpeed = this.m_startMovementSpeed;
            this.m_currentRotationSpeed = this.m_startingRotationSpeed;

            this.m_screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            this.transform.RotateAround(this.transform.position, Vector3.up, this.m_currentRotationSpeed * Time.deltaTime);
            this.m_rigidbody.velocity = new Vector3(this.m_currentMovementSpeed, 0f, 0f);

            if (this.transform.position.x > this.m_screenBounds.x + this.m_xThreshold)
            {
                var pos = this.transform.position;
                pos.x = -this.m_screenBounds.x - this.m_xThreshold;
                this.transform.position = pos;
            }
        }

        private void FixedUpdate()
        {
            this.m_currentTime += Time.fixedDeltaTime;
            this.m_currentMovementSpeed = Mathf.Lerp(this.m_startMovementSpeed, this.m_endMovementSpeed, this.m_currentTime / this.m_reachMaxAfterSeconds);
            this.m_currentRotationSpeed = Mathf.Lerp(this.m_startingRotationSpeed, this.m_endRotatingSpeed, this.m_currentTime / this.m_reachMaxAfterSeconds);
        }
    }
}