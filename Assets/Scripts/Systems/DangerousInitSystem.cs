﻿using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class DangerousInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            foreach (var i in GameObject.FindGameObjectsWithTag("Dangerous"))
            {
                var dangerousEntity = ecsWorld.NewEntity();

                var dangerousPool = ecsWorld.GetPool<DangerousComponent>();
                dangerousPool.Add(dangerousEntity);
                ref var dangerousComponent = ref dangerousPool.Get(dangerousEntity);

                dangerousComponent.obstacleTransform = i.transform;
                dangerousComponent.pointA = i.transform.Find("A").position;
                dangerousComponent.pointB = i.transform.Find("B").position;
            }
        }
    }
}