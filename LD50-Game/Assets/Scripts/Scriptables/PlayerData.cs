using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float m_movementSpeed;
        [SerializeField] private float m_acceleration;
        [SerializeField] private GameObject m_projectile;

        public float MovementSpeed => this.m_movementSpeed;
        public float Acceleration => this.m_acceleration;
        public GameObject Projectile => this.m_projectile;
    }
}