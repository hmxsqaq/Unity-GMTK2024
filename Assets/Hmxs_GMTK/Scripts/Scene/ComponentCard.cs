using Hmxs_GMTK.Scripts.Shape;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class ComponentCard : MonoBehaviour
    {
        [SerializeField] private ShapeComponent component;

        public Transform Position { get; set; }
        public bool IsStored { get; set; } = false;

        public ShapeComponent Component => component;

        public void ReturnToPosition() => transform.position = Position.position;

        private void OnMouseUp()
        {
            // with card in hand, click the container
            if (ComponentContainer.SelectedContainer != null)
                ComponentContainer.SelectedContainer.SetComponent(this);
            else if (transform.position.y < CardManager.Instance.ReturnZoneY) ReturnToPosition();
        }
    }
}