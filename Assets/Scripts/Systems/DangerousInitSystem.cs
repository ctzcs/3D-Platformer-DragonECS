using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class DangerousInitSystem : IEcsInit, IEcsInject<EcsDefaultWorld>
    {
        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;
        public void Init()
        {
            var dangerousPool = _world.GetPool<Dangerous>();

            foreach (var i in GameObject.FindGameObjectsWithTag(Constants.Tags.DangerousTag))
            {
                var dangerousEntity = _world.NewEntity();

                dangerousPool.Add(dangerousEntity);
                ref var dangerousComponent = ref dangerousPool.Get(dangerousEntity);

                dangerousComponent.obstacleTransform = i.transform;
                dangerousComponent.pointA = i.transform.Find("A").position;
                dangerousComponent.pointB = i.transform.Find("B").position;
            }
        }
    }
}
