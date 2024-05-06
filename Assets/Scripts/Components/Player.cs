using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct Player : IEcsComponent
    {
        public Transform playerTransform;
        public Rigidbody playerRB;
        public CapsuleCollider playerCollider;
        public Vector3 playerVelocity;
        public float playerJumpForce;
        public float playerSpeed;
        public int coins;
    }
}