using UnityEngine.InputSystem;

namespace Hmxs_GMTK.Scripts.Input
{
    public static class InputHandler
    {
        public static bool IsClicking => Mouse.current.leftButton.isPressed;
        public static bool IsLeftButtonPressed => Mouse.current.leftButton.wasPressedThisFrame;
        public static bool IsLeftButtonReleased => Mouse.current.leftButton.wasReleasedThisFrame;
    }
}