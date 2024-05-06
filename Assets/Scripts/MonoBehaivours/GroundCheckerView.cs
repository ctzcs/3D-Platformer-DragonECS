using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class GroundCheckerView : MonoBehaviour
    {
        // auto-injected fields.
        public EcsTagPool<IsGrounded> groundedPool;
        public int playerEntity;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (!groundedPool.Has(playerEntity))
                {
                    groundedPool.Add(playerEntity);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (groundedPool.Has(playerEntity))
                {
                    groundedPool.Del(playerEntity);
                }
            }
        }
    }
}