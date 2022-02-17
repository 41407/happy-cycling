using UnityEngine;

namespace Player
{
    public class BikeConfiguration : MonoBehaviour
    {
        #region PhysicsParameters

        [Space] public Vector2
            bodyCenterOfMass = new Vector2(-0.1f, 0.4f);

        [Header("Speed")] public float
            maxSpeed = 5;

        public float maxSpeedLerp = 0.01f;
        public float acceleration = 15;
        public float groundedStaticTorque = 0.1f;
        public float pumpSpeedBoost = 3;
        public float jumpSpeedBoost = 7;

        [Header("Pump")] public float
            groundedPumpTorque = 500;

        public float aerialPumpStrength = -100;

        [Header("Jump")] public float
            ungroundGraceTime = 0.05f;

        public float maxJumpStrength = 2000;
        public float groundedJumpTorque = -50;
        public float aerialJumpTorque = -100;
        public float landStrength = 200;

        #endregion
    }
}
