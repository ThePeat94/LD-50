using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nidavellir
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_poolSegments;
        [SerializeField] private float m_objectDistance;
        [SerializeField] private ObjectSpawner m_objectSpawner;

        private Queue<GameObject> m_orderedSegments;

        private void Awake()
        {
            this.m_orderedSegments = new Queue<GameObject>(this.m_poolSegments);
        }

        public void PutLastToFirstPosition()
        {
            this.ReorderSegments();
        }

        private void ReorderSegments()
        {
            var first = this.m_orderedSegments.Dequeue();
            var last = this.m_orderedSegments.Last();
            first.transform.position = last.transform.position + new Vector3(0, 0, this.m_objectDistance);
            this.m_orderedSegments.Enqueue(first);

            var orderedList = this.m_orderedSegments.ToList();

            foreach (var go in orderedList)
                go.GetComponentInChildren<SegmentEnd>()
                    .GetComponent<Collider>()
                    .enabled = false;
            orderedList[^2]
                .GetComponentInChildren<SegmentEnd>()
                .GetComponent<Collider>()
                .enabled = true;

            this.SetObjectSpawnerPosition(first.transform.position);
        }

        private void SetObjectSpawnerPosition(Vector3 pos)
        {
            this.m_objectSpawner.transform.position = pos;
        }
    }
}