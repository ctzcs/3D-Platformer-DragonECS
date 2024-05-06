using LeopotamGroup.Globals;
using UnityEngine;

namespace Platformer
{
    public class RestartScene : MonoBehaviour
    {
        public void RestartGame()
        {
            Service<SceneService>.Get().ReloadScene();
        }
    }
}