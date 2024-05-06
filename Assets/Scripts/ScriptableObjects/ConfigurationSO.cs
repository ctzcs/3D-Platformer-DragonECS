using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "Configuration")]
    public class ConfigurationSO : ScriptableObject
    {
        public float playerJumpForce;
        public float playerSpeed;
        public float cameraFollowSmoothness;
        public float speedBuffDuration;
        public float jumpBuffDuration;
    }
}