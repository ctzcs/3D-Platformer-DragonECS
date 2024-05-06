using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class JumpBuffSystem : IEcsRun, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
            public EcsPool<JumpBuff> jumpBuffs = Inc;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;

        public void Run()
        {
            foreach (var entity in _world.Where(out Aspect aspect))
            {
                ref var playerComponent = ref aspect.players.Get(entity);
                ref var jumpBuffComponent = ref aspect.jumpBuffs.Get(entity);

                jumpBuffComponent.timer -= Time.deltaTime;

                if (jumpBuffComponent.timer <= 0)
                {
                    playerComponent.playerJumpForce /= 2f;
                    aspect.jumpBuffs.Del(entity);
                }
            }
        }
    }

}
