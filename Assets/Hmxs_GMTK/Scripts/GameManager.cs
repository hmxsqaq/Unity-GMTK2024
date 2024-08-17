using Hmxs.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hmxs_GMTK.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public static Camera MainCamera => Camera.main;

        public static Vector3 GetMouseWorldPosition()
        {
            var camera = MainCamera;
            var mousePosition = Mouse.current.position.ReadValue();
            return camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, camera.nearClipPlane));
        }
    }
}