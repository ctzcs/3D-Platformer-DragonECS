using DCFApixels.DragonECS;
using UnityEngine;


namespace Platformer
{
    public class PlayerJumpSystem : IEcsFixedRunProcess, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
            public EcsPool<PlayerInput> playerInputs = Inc;
            public EcsTagPool<IsGrounded> isGroundeds = Inc;
        }
        class TryJumpAspect : EcsAspect
        {
            public EcsTagPool<TryJump> tryJumps = Inc;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;

        public void FixedRun()
        {
            foreach (var tryJumpEntity in _world.Where(out TryJumpAspect tryJumpAspect))
            {
                _world.DelEntity(tryJumpEntity);
                foreach (var playerEntity in _world.Where(out Aspect aspect))
                {
                    ref var playerComponent = ref aspect.players.Get(playerEntity);

                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}