using DCFApixels.DragonECS;
using LeopotamGroup.Globals;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class EcsRoot : MonoBehaviour
    {
        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private Text coinCounter;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject playerWonPanel;

        private EcsDefaultWorld _world;
        private EcsPipeline _pipeline;

        private void Start()
        {
            _world = new EcsDefaultWorld();

            var gameData = new GameData();
            gameData.configuration = configuration;
            gameData.coinCounter = coinCounter;
            gameData.gameOverPanel = gameOverPanel;
            gameData.playerWonPanel = playerWonPanel;
            gameData.sceneService = Service<SceneService>.Get(true);

            _pipeline = EcsPipeline.New()
                //init only systems
                .Add(new PlayerInitSystem())
                .Add(new DangerousInitSystem())
                //update systems
                .Add(new PlayerInputSystem())
                .Add(new DangerousRunSystem())
                .Add(new CoinHitSystem())
                .Add(new BuffHitSystem())
                .Add(new DangerousHitSystem())
                .Add(new WinHitSystem())
                .Add(new SpeedBuffSystem())
                .Add(new JumpBuffSystem())
                .AutoDel<Hit>()
                //fixed update systems
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerJumpSystem())
                //di
                .Inject(_world, gameData)
                //unity integrations debugger
                .AddUnityDebug(_world)
                .BuildAndInit();
        }

        private void Update()
        {
            _pipeline.Run();
        }

        private void FixedUpdate()
        {
            _pipeline.FixedRun();
        }

        private void OnDestroy()
        {
            _pipeline.Destroy();
            _world.Destroy();
        }
    }
}