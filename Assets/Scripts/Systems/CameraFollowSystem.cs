using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class CameraFollowSystem : IEcsInit, IEcsFixedRunProcess, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class CameraAspect : EcsAspect
        {
            public EcsPool<Camera> cameras = Inc;
        }
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;
        public void Inject(GameData obj) => _gameData = obj;
        GameData _gameData;

        private int cameraEntity;

        public void Init()
        {
            var cameraEntity = _world.NewEntity();
            var cameraAspect = _world.GetAspect<CameraAspect>();
            ref var cameraComponent = ref cameraAspect.cameras.Add(cameraEntity);

            cameraComponent.cameraTransform = UnityEngine.Camera.main.transform;
            cameraComponent.cameraSmoothness = _gameData.configuration.cameraFollowSmoothness;
            cameraComponent.curVelocity = Vector3.zero;
            cameraComponent.offset = new Vector3(0f, 1f, -9f);

            this.cameraEntity = cameraEntity;
        }

        public void FixedRun()
        {
            var cameraAspect = _world.GetAspect<CameraAspect>();
            ref var cameraComponent = ref cameraAspect.cameras.Get(cameraEntity);

            foreach (var entity in _world.Where(out PlayerAspect playerAspect))
            {
                ref var playerComponent = ref playerAspect.players.Get(entity);

                Vector3 currentPosition = cameraComponent.cameraTransform.position;
                Vector3 targetPoint = playerComponent.playerTransform.position + cameraComponent.offset;

                cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
            }
        }
    }
}
