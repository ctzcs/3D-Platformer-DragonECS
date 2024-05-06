using DCFApixels.DragonECS;

namespace Platformer
{
    public class BuffHitSystem : IEcsRun, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class HitAspect : EcsAspect
        {
            public EcsPool<Hit> hits = Inc;
        }
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
            public EcsPool<JumpBuff> jumpBuffs = Opt;
            public EcsPool<SpeedBuff> speedBuffs = Opt;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;
        public void Inject(GameData obj) => _gameData = obj;
        GameData _gameData;


        public void Run()
        {
            var playerEs = _world.Where(out PlayerAspect playerAspect);
            foreach (var hitE in _world.Where(out HitAspect hitAspect))
            {
                ref var hitComponent = ref hitAspect.hits.Get(hitE);

                foreach (var playerE in playerEs)
                {
                    ref var playerComponent = ref playerAspect.players.Get(playerE);

                    if (hitComponent.other.CompareTag(Constants.Tags.SpeedBuffTag))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerSpeed *= 2f;
                        ref var speedBuffComponent = ref playerAspect.speedBuffs.TryAddOrGet(playerE);
                        speedBuffComponent.timer = _gameData.configuration.speedBuffDuration;
                    }

                    if (hitComponent.other.CompareTag(Constants.Tags.JumpBuffTag))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerJumpForce *= 2f;
                        ref var jumpBuffComponent = ref playerAspect.jumpBuffs.TryAddOrGet(playerE);
                        jumpBuffComponent.timer = _gameData.configuration.jumpBuffDuration;
                    }
                }

            }
        }
    }
}
