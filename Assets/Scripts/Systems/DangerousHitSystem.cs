using DCFApixels.DragonECS;

namespace Platformer
{
    public class DangerousHitSystem : IEcsRun, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class HitAspect : EcsAspect
        {
            public EcsPool<Hit> hits = Inc;
        }
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
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

                    if (hitComponent.other.CompareTag(Constants.Tags.DangerousTag))
                    {
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        if (_world.IsUsed(playerE))
                        {
                            _world.DelEntity(playerE);
                        }
                        _gameData.gameOverPanel.SetActive(true);
                    }
                }

            }
        }
    }

}