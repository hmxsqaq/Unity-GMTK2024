using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable2D : MonoBehaviour
    {
        [Title("Info")]
        [SerializeField] [ReadOnly] private bool isDragging;
        [SerializeField] [ReadOnly] private Vector2 objOffset;

        private void OnMouseDown()
        {
            objOffset = transform.position - GameManager.GetMouseWorldPosition();
            isDragging = true;
        }

        private void OnMouseUp()
        {
            isDragging = false;
        }

        private void OnMouseDrag()
        {
            if (!isDragging) return;
            var mousePos = (Vector2)GameManager.GetMouseWorldPosition();
            transform.position = mousePos + objOffset;
        }
    }
}