using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class SpaceShipArsenal : MonoBehaviour
    {
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private SfxData m_projectileFireSfx;
        [SerializeField] private List<Transform> m_projectileSpawns;
        [SerializeField] private ArsenalData m_arsenalData;

        private int m_currentUnlockedCanons;


        private InputProcessor m_inputProcessor;
        private OneShotSfxPlayer m_oneShotSfxPlayer;

        private Coroutine m_shootCoroutine;

        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_oneShotSfxPlayer = this.GetComponent<OneShotSfxPlayer>();
        }

        private void Update()
        {
            if (this.m_inputProcessor.ShootTriggered && this.m_shootCoroutine == null)
                this.m_shootCoroutine = this.StartCoroutine(this.DelayedShooting());
        }

        public void Upgrade()
        {
            this.m_currentUnlockedCanons++;
        }


        private IEnumerator DelayedShooting()
        {
            for (var i = 0; i < this.m_currentUnlockedCanons; i++)
            {
                var index = i % this.m_projectileSpawns.Count;
                this.Shoot(this.m_projectileSpawns[index]);
                yield return new WaitForSeconds(this.m_arsenalData.ShootFrequency);
            }

            yield return new WaitForSeconds(this.m_arsenalData.ShootCooldown);
            this.m_shootCoroutine = null;
        }

        private void Shoot(Transform spawn)
        {
            var instantiated = Instantiate(this.m_playerData.Projectile, spawn.position, spawn.rotation);
            instantiated.GetComponent<Rigidbody>()
                .AddForce(spawn.forward * 50, ForceMode.Impulse);
            this.m_oneShotSfxPlayer.PlayOneShot(this.m_projectileFireSfx);
        }
    }
}