using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class PlayerMoveSystem : IEcsFixedRunProcess, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
            public EcsPool<PlayerInput> playerInputs = Inc;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;

        public void FixedRun()
        {
            foreach (var entity in _world.Where(out Aspect aspect))
            {
                ref var playerComponent = ref aspect.players.Get(entity);
                ref var playerInputComponent = ref aspect.playerInputs.Get(entity);

                playerComponent.playerRB.AddForce(playerInputComponent.moveInput * playerComponent.playerSpeed, ForceMode.Acceleration);
            }

        }
    }
}