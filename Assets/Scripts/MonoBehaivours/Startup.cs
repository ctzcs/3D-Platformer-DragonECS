﻿using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class Startup : MonoBehaviour
    {
        private EcsWorld ecsWorld;
        private EcsSystems initSystems;
        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystems;
        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private Text coinCounter;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject playerWonPanel;

        private void Start()
        {
            ecsWorld = new EcsWorld();
            var gameData = new GameData();

            gameData.configuration = configuration;
            gameData.coinCounter = coinCounter;
            gameData.gameOverPanel = gameOverPanel;
            gameData.playerWonPanel = playerWonPanel;

            initSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerInitSystem())
                .Add(new DangerousInitSystem());

            initSystems.Init();

            updateSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerInputSystem())
                .Add(new DangerousRunSystem())
                .Add(new HitSystem())
                .Add(new SpeedBuffSystem())
                .Add(new JumpBuffSystem())
                .DelHere<HitComponent>();

            updateSystems.Init();

            fixedUpdateSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerJumpSystem());

            fixedUpdateSystems.Init();
        }

        private void Update()
        {
            updateSystems.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            initSystems.Destroy();
            updateSystems.Destroy();
            fixedUpdateSystems.Destroy();
            ecsWorld.Destroy();
        }
    }
}