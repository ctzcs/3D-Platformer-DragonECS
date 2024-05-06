using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class PlayerInitSystem : IEcsInit, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;
        public void Inject(GameData obj) => _gameData = obj;
        GameData _gameData;

        public void Init()
        {
            var playerEntity = _world.NewEntity();

            var playerPool = _world.GetPool<Player>();
            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);
            var playerInputPool = _world.GetPool<PlayerInput>();
            playerInputPool.Add(playerEntity);
            ref var playerInputComponent = ref playerInputPool.Get(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            playerGO.GetComponentInChildren<GroundCheckerView>().groundedPool = _world.GetPool<IsGrounded>();
            playerGO.GetComponentInChildren<GroundCheckerView>().playerEntity = playerEntity;
            playerGO.GetComponentInChildren<CollisionCheckerView>().ecsWorld = _world;
            playerComponent.playerSpeed = _gameData.configuration.playerSpeed;
            playerComponent.playerTransform = playerGO.transform;
            playerComponent.playerJumpForce = _gameData.configuration.playerJumpForce;
            playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.playerRB = playerGO.GetComponent<Rigidbody>();
        }
    }
}