using System.Collections.Generic;
using System.Linq;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private List<ObjectSpawnerData> m_spawnerData;
        private BoxCollider m_collider;

        private float m_framesSinceLastSpawn;

        private float m_lastSpawnPositionForX;
        private float m_maxX;
        private float m_minX;

        private Dictionary<ObjectSpawnerData, int> m_pastFramesSinceLastSpawn;

        private void Awake()
        {
            this.m_collider = this.GetComponent<BoxCollider>();
            this.m_maxX = this.m_collider.bounds.extents.x;
            this.m_minX = -this.m_collider.bounds.extents.x;

            this.m_pastFramesSinceLastSpawn = this.m_spawnerData.ToDictionary(o => o, o => 0);
        }

        private void FixedUpdate()
        {
            foreach (var key in this.m_pastFramesSinceLastSpawn.Keys.ToList())
            {
                if (this.m_pastFramesSinceLastSpawn[key] >= key.FrameCoolDown)
                {
                    this.SpawnObject(key);
                    this.m_pastFramesSinceLastSpawn[key] = 0;
                }
                else
                {
                    this.m_pastFramesSinceLastSpawn[key]++;
                }
            }
        }

        private float GetRandomPositionForX()
        {
            return Random.Range(this.m_minX, this.m_maxX);
        }

        private Vector3 GetRandomTorque(ObjectSpawnerData data)
        {
            return new Vector3(Random.Range(data.MinRotationSpeed, data.MaxRotationSpeed + 1), Random.Range(data.MinRotationSpeed, data.MaxRotationSpeed + 1),
                Random.Range(data.MinRotationSpeed, data.MaxRotationSpeed + 1));
        }

        private float GetRandomVelocity(ObjectSpawnerData data)
        {
            return Random.Range(data.MinVelocity, data.MaxVelocity + 1);
        }

        private void SpawnObject(ObjectSpawnerData data)
        {
            var spawned = Instantiate(data.ToSpawn, new Vector3(this.GetRandomPositionForX(), this.transform.position.y, this.transform.position.z), Quaternion.identity);
            var asteroid = spawned.GetComponent<SpawnableObject>();
            asteroid.SetConstantVelocity(new Vector3(0f, 0f, -this.transform.forward.z * this.GetRandomVelocity(data)));
            asteroid.SetRandomRotation(this.GetRandomTorque(data));
        }
    }
}