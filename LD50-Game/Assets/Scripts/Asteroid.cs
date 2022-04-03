using UnityEngine;

namespace Nidavellir
{
    public class Asteroid : MonoBehaviour, IHittable
    {
        [SerializeField] private int m_shieldDamage;
        [SerializeField] private RandomClipPlayer m_randomExplodePlayer;
        [SerializeField] private RandomClipPlayer m_randomPlayerHitPlayer;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ShieldController>(out var shieldController))
            {
                Debug.Log("Asteroid has hit player");
                shieldController.InflictDamage(this.m_shieldDamage);
                this.DisableMesh();
                this.m_randomPlayerHitPlayer.PlayRandomOneShot();
                Destroy(this.gameObject, 2f);
            }
        }

        public void Hit()
        {
            this.DisableMesh();
            this.m_randomExplodePlayer.PlayRandomOneShot();
            Destroy(this.gameObject, 2f);
        }

        private void DisableMesh()
        {
            this.GetComponentInChildren<Collider>()
                .enabled = false;
            this.GetComponentInChildren<MeshRenderer>()
                .enabled = false;
        }
    }
}