using System.Collections.Generic;
using static DCFApixels.DragonECS.EcsOneFrameComponentConsts;

namespace DCFApixels.DragonECS
{
    [MetaTags(MetaTags.HIDDEN)]
    [MetaColor(MetaColor.Grey)]
    public class DeleteOneFrameComponentSystem<TComponent> : IEcsRun, IEcsInject<EcsWorld>
        where TComponent : struct, IEcsComponent
    {
        private sealed class Aspect : EcsAspect
        {
            public EcsPool<TComponent> pool = Inc;
        }
        private readonly List<EcsWorld> _worlds = new List<EcsWorld>();
        public void Inject(EcsWorld obj) => _worlds.Add(obj);
        public void Run()
        {
            for (int i = 0, iMax = _worlds.Count; i < iMax; i++)
            {
                EcsWorld world = _worlds[i];
                if (world.IsComponentTypeDeclared<TComponent>())
                {
                    foreach (var e in world.Where(out Aspect a))
                    {
                        a.pool.Del(e);
                    }
                }
            }
        }
    }
    [MetaTags(MetaTags.HIDDEN)]
    [MetaColor(MetaColor.Grey)]
    public class DeleteOneFrameTagComponentSystem<TComponent> : IEcsRun, IEcsInject<EcsWorld>
    where TComponent : struct, IEcsTagComponent
    {
        private sealed class Aspect : EcsAspect
        {
            public EcsTagPool<TComponent> pool = Inc;
        }
        private readonly List<EcsWorld> _worlds = new List<EcsWorld>();
        public void Inject(EcsWorld obj) => _worlds.Add(obj);
        public void Run()
        {
            for (int i = 0, iMax = _worlds.Count; i < iMax; i++)
            {
                EcsWorld world = _worlds[i];
                if (world.IsComponentTypeDeclared<TComponent>())
                {
                    foreach (var e in world.Where(out Aspect a))
                    {
                        a.pool.Del(e);
                    }
                }
            }
        }
    }

    public static class EcsOneFrameComponentConsts
    {
        public const string AUTO_DEL_LAYER = nameof(AUTO_DEL_LAYER);
    }
    public static class DeleteOneFrameComponentSystemExtensions
    {
        public static EcsPipeline.Builder AutoDel<TComponent>(this EcsPipeline.Builder b, string layerName = null)
            where TComponent : struct, IEcsComponent
        {
            if (AUTO_DEL_LAYER == layerName)
            {
                b.Layers.InsertAfter(EcsConsts.POST_END_LAYER, AUTO_DEL_LAYER);
            }
            b.AddUnique(new DeleteOneFrameComponentSystem<TComponent>(), layerName);
            return b;
        }
        public static EcsPipeline.Builder AutoDelToEnd<TComponent>(this EcsPipeline.Builder b)
            where TComponent : struct, IEcsComponent
        {
            b.Layers.InsertAfter(EcsConsts.POST_END_LAYER, AUTO_DEL_LAYER);
            b.AddUnique(new DeleteOneFrameComponentSystem<TComponent>(), AUTO_DEL_LAYER);
            return b;
        }
    }
    public static class DeleteOneFrameTagComponentSystemExtensions
    {
        public static EcsPipeline.Builder AutoDel<TComponent>(this EcsPipeline.Builder b, string layerName = null)
            where TComponent : struct, IEcsTagComponent
        {
            if (AUTO_DEL_LAYER == layerName)
            {
                b.Layers.InsertAfter(EcsConsts.POST_END_LAYER, AUTO_DEL_LAYER);
            }
            b.AddUnique(new DeleteOneFrameTagComponentSystem<TComponent>(), layerName);
            return b;
        }
        public static EcsPipeline.Builder AutoDelToEnd<TComponent>(this EcsPipeline.Builder b)
            where TComponent : struct, IEcsTagComponent
        {
            b.Layers.InsertAfter(EcsConsts.POST_END_LAYER, AUTO_DEL_LAYER);
            b.AddUnique(new DeleteOneFrameTagComponentSystem<TComponent>(), AUTO_DEL_LAYER);
            return b;
        }
    }
}