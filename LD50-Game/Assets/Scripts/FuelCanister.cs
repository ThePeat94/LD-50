using Nidavellir.ResourceControllers;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class FuelCanister : MonoBehaviour
    {
        [SerializeField] private float m_amount;
        [SerializeField] private SfxData m_collectedSfx;

        private OneShotSfxPlayer m_oneShotSfxPlayer;

        private void Awake()
        {
            this.m_oneShotSfxPlayer = this.GetComponent<OneShotSfxPlayer>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<FuelResourceController>(out var fuelController))
            {
                fuelController.AddCanister(this.m_amount);
                this.DisableMesh();
                this.m_oneShotSfxPlayer.PlayOneShot(this.m_collectedSfx);
                Destroy(this.gameObject, this.m_collectedSfx.AudioClip.length);
            }
        }

        private void DisableMesh()
        {
            this.GetComponentInChildren<Collider>()
                .enabled = false;

            var renderers = this.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in renderers)
                meshRenderer.enabled = false;

            var canvasses = this.GetComponentsInChildren<Canvas>();
            foreach (var canvas in canvasses)
                canvas.gameObject.SetActive(false);
        }
    }
}