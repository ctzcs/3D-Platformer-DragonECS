using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class PlayerInputSystem : IEcsRun, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<PlayerInput> playerInputs = Inc;
            public EcsTagPool<TryJump> tryJumps = Opt;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;
        public void Inject(GameData obj) => _gameData = obj;
        GameData _gameData;

        public void Run()
        {

            foreach (var entity in _world.Where(out Aspect aspect))
            {
                ref var playerInputComponent = ref aspect.playerInputs.Get(entity);

                playerInputComponent.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    var tryJump = _world.NewEntity();
                    aspect.tryJumps.Add(tryJump);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    _gameData.sceneService.ReloadScene();
                }
            }
        }
    }
}