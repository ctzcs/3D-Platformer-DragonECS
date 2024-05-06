using DCFApixels.DragonECS;
using UnityEngine;


namespace Platformer
{
    public class SpeedBuffSystem : IEcsRun, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
            public EcsPool<SpeedBuff> speedBuffs = Inc;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;

        public void Run()
        {
            foreach (var entity in _world.Where(out Aspect aspect))
            {
                ref var playerComponent = ref aspect.players.Get(entity);
                ref var speedBuffComponent = ref aspect.speedBuffs.Get(entity);

                speedBuffComponent.timer -= Time.deltaTime;

                if (speedBuffComponent.timer <= 0)
                {
                    playerComponent.playerSpeed /= 2f;
                    aspect.speedBuffs.Del(entity);
                }
            }
        }
    }
}