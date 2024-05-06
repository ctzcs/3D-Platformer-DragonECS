using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct Camera : IEcsComponent
    {
        public Transform cameraTransform;
        public Vector3 curVelocity;
        public Vector3 offset;
        public float cameraSmoothness;
    }
}