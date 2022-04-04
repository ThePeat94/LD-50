using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class ArsenalUpgrade : MonoBehaviour
    {
        [SerializeField] private SfxData m_collectedSfx;

        private OneShotSfxPlayer m_oneShotSfxPlayer;

        private void Awake()
        {
            this.m_oneShotSfxPlayer = this.GetComponent<OneShotSfxPlayer>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<SpaceShipArsenal>(out var arsenal))
            {
                arsenal.Upgrade();
                this.DisableMesh();
                this.m_oneShotSfxPlayer.PlayOneShot(this.m_collectedSfx);
                Destroy(this.gameObject.transform.parent, this.m_collectedSfx.AudioClip.length);
            }
        }

        private void DisableMesh()
        {
            this.GetComponentInChildren<Collider>()
                .enabled = false;

            var meshRenderer = this.GetComponentsInChildren<MeshRenderer>();

            foreach (var renderer in meshRenderer)
                renderer.enabled = false;
        }
    }
}