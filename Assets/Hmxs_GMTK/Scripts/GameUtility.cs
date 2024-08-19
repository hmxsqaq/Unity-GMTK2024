using UnityEngine;

namespace Hmxs_GMTK.Scripts
{
    public static class GameUtility
    {
        public static Camera MainCamera => Camera.main;

        public static Vector3 GetMouseWorldPosition()
        {
            var camera = MainCamera;
            var mousePosition = Input.mousePosition;
            return camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, camera.nearClipPlane));
        }

        public static Vector2 GetMouseScreenPosition() => Input.mousePosition;
    }
}