using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerStatsManager : MonoBehaviour
    {
        [SerializeField] private PlayerData m_playerData;

        private List<PlayerStats> m_currentEffects;

        public PlayerStats PlayerStats { get; private set; }

        private void Awake()
        {
            this.m_currentEffects = new List<PlayerStats>();
            this.PlayerStats = new PlayerStats(this.m_playerData);
        }

        public void EffectWithGivenDelta(PlayerStats delta)
        {
            this.PlayerStats.Acceleration += delta.Acceleration;
            this.PlayerStats.MaxMovementSpeed += delta.MaxMovementSpeed;
        }

        public void RemoveEffect(PlayerStats delta)
        {
            this.PlayerStats.Acceleration -= delta.Acceleration;
            this.PlayerStats.MaxMovementSpeed -= delta.MaxMovementSpeed;
        }
    }
}