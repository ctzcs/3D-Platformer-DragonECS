using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct Dangerous : IEcsComponent
    {
        public Transform obstacleTransform;
        public Vector3 pointA;
        public Vector3 pointB;
    }
}