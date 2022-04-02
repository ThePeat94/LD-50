using UnityEngine;

namespace Nidavellir
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_asteroidPrefab;
        private BoxCollider m_collider;

        private float m_framesSinceLastSpawn;

        private float m_lastSpawnPositionForX;
        private float m_maxX;
        private float m_minX;

        private void Awake()
        {
            this.m_collider = this.GetComponent<BoxCollider>();
            this.m_maxX = this.m_collider.bounds.extents.x;
            this.m_minX = -this.m_collider.bounds.extents.x;
        }

        private void FixedUpdate()
        {
            this.m_framesSinceLastSpawn++;
            if (this.m_framesSinceLastSpawn >= 30)
            {
                this.SpawnAsteroid();
                this.m_framesSinceLastSpawn = 0;
            }
        }

        private float GetRandomPositionForX()
        {
            return Random.Range(this.m_minX, this.m_maxX);
        }

        private Vector3 GetRandomTorque()
        {
            return new(Random.Range(-180f, 181f), Random.Range(-180f, 181f), Random.Range(-180f, 181f));
        }

        private float GetRandomVelocity()
        {
            return Random.Range(5f, 31f);
        }

        private void SpawnAsteroid()
        {
            var spawned = Instantiate(this.m_asteroidPrefab, new Vector3(this.GetRandomPositionForX(), this.transform.position.y, this.transform.position.z), Quaternion.identity);
            var asteroid = spawned.GetComponent<Asteroid>();
            asteroid.SetConstantVelocity(new Vector3(0f, 0f, -this.transform.forward.z * this.GetRandomVelocity()));
            asteroid.SetRandomRotation(this.GetRandomTorque());
        }
    }
}