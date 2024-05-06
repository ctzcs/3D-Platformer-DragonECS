using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class DangerousRunSystem : IEcsRun, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Dangerous> dangerouses = Inc;
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        EcsDefaultWorld _world;

        public void Run()
        {
            foreach (var entity in _world.Where(out Aspect aspect))
            {
                ref var dangerousComponent = ref aspect.dangerouses.Get(entity);
                Vector3 pos1 = dangerousComponent.pointA;
                Vector3 pos2 = dangerousComponent.pointB;

                dangerousComponent.obstacleTransform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time, 1.0f));
            }
        }
    }
}