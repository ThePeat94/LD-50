using Scriptables;

namespace Nidavellir
{
    public class PlayerStats
    {
        public PlayerStats(PlayerData playerData)
        {
            this.Acceleration = playerData.Acceleration;
            this.MaxMovementSpeed = playerData.MovementSpeed;
        }

        public PlayerStats()
        {
        }

        public float Acceleration { get; set; }
        public float MaxMovementSpeed { get; set; }
        public float FuelTankSize { get; set; }
        public float ShieldSize { get; set; }
    }
}