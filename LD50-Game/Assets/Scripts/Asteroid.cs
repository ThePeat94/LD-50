using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class Asteroid : MonoBehaviour, IHittable
    {
        [SerializeField] private int m_shieldDamage;
        [SerializeField] private SfxData m_explodeSfx;
        [SerializeField] private SfxData m_hitPlayerSfx;


        private OneShotSfxPlayer m_oneShotSfxPlayer;

        private void Awake()
        {
            this.m_oneShotSfxPlayer = this.GetComponent<OneShotSfxPlayer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ShieldController>(out var shieldController))
            {
                Debug.Log("Asteroid has hit player");
                shieldController.InflictDamage(this.m_shieldDamage);
                this.DisableMesh();
                this.PlaySfx(this.m_hitPlayerSfx);
                Destroy(this.gameObject, 2f);
            }
        }

        public void Hit()
        {
            this.DisableMesh();
            this.PlaySfx(this.m_explodeSfx);
            Destroy(this.gameObject, 2f);
        }

        private void DisableMesh()
        {
            this.GetComponentInChildren<Collider>()
                .enabled = false;
            this.GetComponentInChildren<MeshRenderer>()
                .enabled = false;
        }

        private void PlaySfx(SfxData toPlay)
        {
            this.m_oneShotSfxPlayer.PlayOneShot(toPlay);
        }
    }
}